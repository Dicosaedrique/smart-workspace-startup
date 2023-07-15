﻿using System.Globalization;
using FastWorkspace.Domain.Common.Localization;
using FastWorkspace.Domain.Services;

namespace FastWorkspace.Client.Common.Services;

public class AppInitializeService : IMauiInitializeService
{
    private IAppConfiguration _appConfiguration = null!;
    private IWorkspaceStore _workspaceStore = null!;

    public void Initialize(IServiceProvider services)
    {
        _appConfiguration = services.GetRequiredService<IAppConfiguration>();
        _workspaceStore = services.GetRequiredService<IWorkspaceStore>();

        _appConfiguration.Reset();

        SetupStores();
        SetupAppLanguage();
    }

    private void SetupStores()
    {
        if (_appConfiguration.GetFileStorageDirectoryPath() != null)
        {
            _workspaceStore.SetupStore();
        }
    }

    private void SetupAppLanguage()
    {
        var language = _appConfiguration.GetLanguage();

        CultureInfo.CurrentCulture = language.GetCulture();
        CultureInfo.DefaultThreadCurrentCulture = CultureInfo.CurrentCulture;
        CultureInfo.DefaultThreadCurrentUICulture = CultureInfo.CurrentCulture;
    }
}
