using System.Text.Json.Serialization;

namespace PayForBlob.Dto;

public class TxResult
{
  [JsonPropertyName("height")]
  public long Height { get; set; }

  [JsonPropertyName("txhash")]
  public string? Txhash { get; set; }

  [JsonPropertyName("raw_log")]
  public string? Log { get; set; }
}
