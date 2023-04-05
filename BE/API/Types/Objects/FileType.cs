using System.ComponentModel.DataAnnotations;

namespace API.Types.Objects;

#region REQUEST

public class ReqUploadSmallFile
{
    [Required] public IFormFile File { get; set; }

    public string? Folder { get; set; }
}

public class ReqDownloadFile
{
    [Required] public string Key { get; set; }
    public int? ExpireTimeInSecond { get; set; }
}

public class ReqSharedFile
{
    [Required] public string Hash { get; set; }
}

public class ReqDeleteFile
{
    [Required] public string Key { get; set; }
}

#endregion

#region OTHERS

public class UploadFileDto
{
    public string Key { get; set; }
    public Stream Stream { get; set; }
    public string? ContentType { get; set; }
    public Dictionary<string, string> Metadata { get; set; }
}

public class DownloadFileDto
{
    public string Bucket { get; set; }
    public string Key { get; set; }
}

#endregion

#region RESPONSE

public class ResStatFile
{
    public string Key { get; set; }
    public long Size { get; set; }
}

public class ResDetailStatFile
{
    public string Key { get; set; }
    public long Size { get; set; }
    public string ContentType { get; set; }
    public Dictionary<string, string> MetaData { get; set; }
}

public class ResUploadFile
{
    public string Bucket { get; set; }
    public string Key { get; set; }
    public string OldName { get; set; }
}

#endregion
