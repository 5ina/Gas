using Abp.Application.Navigation;
using Abp.Localization;

namespace GasSolution.Web
{
    /// <summary>
    /// This class defines menus for the application.
    /// It uses ABP's menu system.
    /// When you add menu items here, they are automatically appear in angular application.
    /// See Views/Layout/_TopMenu.cshtml file to know how to render menu.
    /// </summary>
    public class GasSolutionNavigationProvider : NavigationProvider
    {
        public override void SetNavigation(INavigationProviderContext context)
        {
            context.Manager.MainMenu
                .AddItem(
                    new MenuItemDefinition(
                        "Home",
                        new LocalizableString("HomePage", GasSolutionConsts.LocalizationSourceName),
                        url: "",
                        icon: "fa fa-home"
                        )
                ).AddItem(
                    new MenuItemDefinition(
                        "Gas",
                        new LocalizableString("GasPage", GasSolutionConsts.LocalizationSourceName),
                        url: "Gas",
                        icon: "fa fa-send-o"
                        )
                );
        }
    }
}
