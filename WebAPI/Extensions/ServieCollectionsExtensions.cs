namespace FileAssistant.Extensions;

public static class ServieCollectionsExtensions
{
  /// <summary>
  /// 添加用户数据服务配置
  /// </summary>
  /// <param name="services"></param>
  /// <param name="configure"></param>
  /// <returns></returns>
  public static IServiceCollection AddUserFile(this IServiceCollection services,
        Action<UserFileOptions> configure)
  {
    // 检查参数
    ArgumentNullException.ThrowIfNull(configure, nameof(configure));

    var options = new UserFileOptions();

    configure(options);

    ValidateOptions(options);
    EnsureStorageDirectoryExists(options.ResolvedStoragePath);

    services.AddSingleton(options);
    return services;
  }

  /// <summary>
  /// 验证配置选项
  /// </summary>
  /// <param name="options"></param>
  /// <exception cref="ArgumentException"></exception>
  private static void ValidateOptions(UserFileOptions options)
  {
    if (string.IsNullOrWhiteSpace(options.StoragePath))
      throw new ArgumentException("StoragePath cannot be null or empty.");
  }

  /// <summary>
  /// 确保存储目录存在
  /// </summary>
  /// <param name="resolvedStoragePath"></param>
  /// <exception cref="InvalidOperationException"></exception>
  private static void EnsureStorageDirectoryExists(string resolvedStoragePath)
  {
    try
    {
      if (!Directory.Exists(resolvedStoragePath))
      {
        Directory.CreateDirectory(resolvedStoragePath);
        Console.WriteLine($"Created directory: {resolvedStoragePath}");
      }
      else
      {
        Console.WriteLine($"Directory already exists: {resolvedStoragePath}");
      }
    }
    catch (Exception ex)
    {
      // 在这里捕获「创建目录失败」的异常，给出更精准的提示
      throw new InvalidOperationException(
         $"Failed to create or access the storage directory at '{resolvedStoragePath}'.", ex);
    }
  }
}
