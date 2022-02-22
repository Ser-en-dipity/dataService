using System;
using Microsoft.AspNetCore.Authorization;

namespace Authorization;

public class AuthPolicy{
    public const string RequireAdmin = "RequireAdmin";
    public const string RequireCust = "RequireCust";

    public static Action<AuthorizationOptions> DefaultPolicy => options =>{
        options.AddPolicy(RequireAdmin, policy => {
            policy.RequireAuthenticatedUser();
            policy.RequireRole("super_admin", "admin");
        });
        options.AddPolicy(RequireCust, policy => {
            policy.RequireAuthenticatedUser();
            policy.RequireRole("super_admin", "admin");
        });
    };
}