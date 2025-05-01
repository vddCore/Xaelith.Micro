namespace Xaelith.Micro.Visual.Pages.Base;

using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using Xaelith.Micro.Infrastructure.DataModel.Core.Security;

[Authorize]
public partial class XaelithRestrictedPage
{
}