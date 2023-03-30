// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using Api.Modules.Category.Type;
using AutoMapper;

namespace Api.Modules.Category.Mapping;

public class CategoryProfile : Profile
{
    public CategoryProfile()
    {
        CreateMap<Context.Entities.Category, CategoryRes>();

        CreateMap<CreateCategoryReq, Context.Entities.Category>();
    }
}
