using shortid.Configuration;

namespace Stump.Storage.Types.Constant;

public static class GenHashOptions
{
    public static GenerationOptions FileKey =
        new GenerationOptions(useNumbers: true, useSpecialCharacters: true, length: 14);

    public static GenerationOptions ShareLink =
        new GenerationOptions(useNumbers: true, length: 10);
}