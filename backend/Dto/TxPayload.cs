using System.Text.Json.Serialization;

namespace PayForBlob.Dto;

public class TxPayload
{
  [JsonPropertyName("namespace_id")]
  public string? NamespaceId { get; set; }


  [JsonPropertyName("data")]
  public string? Data { get; set; }


  [JsonPropertyName("gas_limit")]
  public int GasLimit { get; set; }


  [JsonPropertyName("fee")]
  public int Fee { get; set; }
}
