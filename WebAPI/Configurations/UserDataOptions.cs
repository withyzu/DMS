namespace FileAssistant.Configurations;

/// <summary>
/// 用户数据设置
/// </summary>
public class UserFileOptions
{
  /// <summary>
  /// 数据存储路径
  /// </summary>
  public string StoragePath { get; set; } = Path.Combine(AppContext.BaseDirectory, "UserFile");


  /// <summary>
  /// 解析后的绝对路径
  /// </summary>
  public string ResolvedStoragePath => Path.GetFullPath(StoragePath);
}
