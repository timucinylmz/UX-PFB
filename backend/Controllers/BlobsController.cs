using System.Numerics;
using System.Text;
using System.Text.Json;
using Microsoft.AspNetCore.Mvc;
using PayForBlob.Dto;

namespace PayForBlob.Controllers;

[ApiController]
[Route("api/[controller]")]
public class BlobsController : ControllerBase
{

  private static readonly string EndPointAddress = "http://localhost:26659/submit_pfb";

  private static HttpClient httpClient = new()
  {
    BaseAddress = new Uri(EndPointAddress),
  };

  [HttpGet]
  [Route("calculate")]
  public OpResult CalculateHex(long seed)
  {
    Random r = new Random((int)seed);
    byte[] b = new byte[8];

    r.NextBytes(b);

    var namespaceId = ConvertToHexString(b);

    var lenMsg = r.Next(100) + 1; // generate a positive integer between 1 and 100

    var msgBuffer = new byte[lenMsg];
    r.NextBytes(msgBuffer);
    var msg = ConvertToHexString(msgBuffer);

    return new OpResult { Success = true, Data = new { nsId = namespaceId, msg = msg } };
  }


  [HttpPost]
  [Route("tx")]
  public async Task<OpResult> MakeTx([FromBody] TxPayload payload)
  {

    try
    {
      using StringContent jsonContent = new(
      JsonSerializer.Serialize(payload),
      Encoding.UTF8,
      "application/json");


      using HttpResponseMessage response = await httpClient.PostAsync("http://localhost:26659/submit_pfb", jsonContent);

      var jsonResponse = await response.Content.ReadAsStringAsync();
      // Console.WriteLine(jsonResponse);

      var txResult = JsonSerializer.Deserialize<TxResult>(jsonResponse);


      return txResult?.Height > 0 ? new OpResult { Success = true, Data = txResult }
          : new OpResult { Success = false, Message = txResult?.Log };
    }
    catch (Exception e)
    {
      return new OpResult { Success = false, Message = e.ToString() };
    }

  }

  private static string ConvertToHexString(byte[] data)
  {
    return BitConverter.ToString(data).Replace("-", "").ToLower();
  }


}
