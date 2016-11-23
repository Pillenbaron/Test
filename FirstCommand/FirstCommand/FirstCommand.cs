//------------------------------------------------------------------------------
// <copyright file="FirstCommand.cs" company="Company">
//     Copyright (c) Company.  All rights reserved.
// </copyright>
//------------------------------------------------------------------------------

using System;
using System.ComponentModel.Design;
using System.Globalization;
using Microsoft.VisualStudio.Shell;
using Microsoft.VisualStudio.Shell.Interop;
using System.Diagnostics;
using EnvDTE;
using Microsoft.VisualStudio.CommandBars;

namespace FirstCommand
{
    /// <summary>
    /// Command handler
    /// </summary>
    internal sealed class FirstCommand
    {
        /// <summary>
        /// Command ID.
        /// </summary>
        public const int CommandId = 0x0100;

        /// <summary>
        /// Command menu group (command set GUID).
        /// </summary>
        public static readonly Guid CommandSet = new Guid("880fdc35-6cba-42ca-b9e7-016a595b9450");

        /// <summary>
        /// VS Package that provides this command, not null.
        /// </summary>
        private readonly Package package;

        /// <summary>
        /// Initializes a new instance of the <see cref="FirstCommand"/> class.
        /// Adds our command handlers for menu (commands must exist in the command table file)
        /// </summary>
        /// <param name="package">Owner package, not null.</param>
        private FirstCommand(Package package)
        {
            if (package == null)
            {
                throw new ArgumentNullException("package");
            }

            this.package = package;

            OleMenuCommandService commandService = this.ServiceProvider.GetService(typeof(IMenuCommandService)) as OleMenuCommandService;
            if (commandService != null)
            {
                var menuCommandID = new CommandID(CommandSet, CommandId);
                var menuItem = new MenuCommand(this.StartNotepad, menuCommandID);
                commandService.AddCommand(menuItem);
            }
        }

        /// <summary>
        /// Gets the instance of the command.
        /// </summary>
        public static FirstCommand Instance
        {
            get;
            private set;
        }

        /// <summary>
        /// Gets the service provider from the owner package.
        /// </summary>
        private IServiceProvider ServiceProvider
        {
            get
            {
                return this.package;
            }
        }

        /// <summary>
        /// Initializes the singleton instance of the command.
        /// </summary>
        /// <param name="package">Owner package, not null.</param>
        public static void Initialize(Package package)
        {
            Instance = new FirstCommand(package);
        }

        ///// <summary>
        ///// This function is the callback used to execute the command when the menu item is clicked.
        ///// See the constructor to see how the menu item is associated with this function using
        ///// OleMenuCommandService service and MenuCommand class.
        ///// </summary>
        ///// <param name="sender">Event sender.</param>
        ///// <param name="e">Event args.</param>
        //private void MenuItemCallback(object sender, EventArgs e)
        //{
        //    string message = string.Format(CultureInfo.CurrentCulture, "Inside {0}.MenuItemCallback()", this.GetType().FullName);
        //    string title = "FirstCommand";

        //    // Show a message box to prove we were here
        //    VsShellUtilities.ShowMessageBox(
        //        this.ServiceProvider,
        //        message,
        //        title,
        //        OLEMSGICON.OLEMSGICON_INFO,
        //        OLEMSGBUTTON.OLEMSGBUTTON_OK,
        //        OLEMSGDEFBUTTON.OLEMSGDEFBUTTON_FIRST);
        //}

        private void StartNotepad(object sender, EventArgs e)
        {

            DTE _Service = (DTE)this.ServiceProvider.GetService(typeof(DTE));

            CommandBars commandBars;

            try
            {

                // The following cast is required in VS 2005 and higher because its DTE.CommandBars returns the type Object
                // (because VS 2005 and higher uses for commandbars the type Microsoft.VisualStudio.CommandBars.CommandBars 
                // of the new Microsoft.VisualStudio.CommandBars.dll assembly while VS.NET 2002/2003 used the 
                // type Microsoft.Office.Core.CommandBars of the Office.dll assembly)

                commandBars = (CommandBars)_Service.CommandBars;

                foreach (CommandBar commandBar in commandBars)
                {
                    //using (System.IO.StreamWriter file = new System.IO.StreamWriter(@"C:\VisualStudioCommandBars.txt"))
                    //{

                        foreach (CommandBarControl commandBarControl1 in commandBar.Controls)
                        {

                            try
                            {
                                System.Diagnostics.Debug.Print("----------------------------------------");
                                System.Diagnostics.Debug.Print($"Candidate CommandBar Name: {commandBar.Name}");
                                System.Diagnostics.Debug.Print("Captions on this command bar:");

                                foreach (CommandBarControl commandBarControl2 in commandBar.Controls)
                                {
                                    System.Diagnostics.Debug.Print($"Command: {commandBarControl2.Caption}");
                                }
                            }
                            catch (Exception ex)
                            {

                                System.Diagnostics.Debug.Print($"Error @: {commandBar.Name}/{ex.ToString()}");
                            }



                        }

                    //}

                }

            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.Print(ex.ToString());
            }

            //DTE _Service = (DTE)this.ServiceProvider.GetService(typeof(DTE));
            ////_Service.Events.BuildEvents.OnBuildDone += _BuildDone;
            ////SolutionBuild solBuild = _Service.Solution.SolutionBuild;
            ////solBuild.ActiveConfiguration.Activate();
            ////solBuild.Build(false);

            //Properties PropertieList = null;

            //foreach (Project cProject in _Service.Solution.Projects)
            //{

            //    PropertieList = cProject.ConfigurationManager.ActiveConfiguration.Properties;
            //    break;

            //}

            ////Projects projects = _Project.ConfigurationManager.ActiveConfiguration.Properties.Item(if);

            ////Solution.Model.ProjectData Project = new Solution.Model.ProjectData();

            ////Project.Assembly.Titel = projects.Item(i).DTE.

            //foreach (Property property in PropertieList)
            //{
            //    try
            //    {

            //        System.Diagnostics.Debug.Print($"{property.Name}|{property.Value.ToString()}|{property.Value.GetType().ToString()}");

            //    }
            //    catch (Exception)
            //    {

            //        System.Diagnostics.Debug.Print("Fehler!");

            //    }

            //}

            //////System.Windows.MessageBox.Show(Projects);

        }

        public void _BuildDone(vsBuildScope Scope, vsBuildAction Action)
        {

            DTE _Service = (DTE)this.ServiceProvider.GetService(typeof(DTE));


            try
            {

                if (Scope == vsBuildScope.vsBuildScopeSolution)
                {

                    switch (Action)
                    {
                        case vsBuildAction.vsBuildActionBuild:
                            System.Windows.MessageBox.Show($"{Scope.ToString()}/{Action.ToString()}");
                            break;
                        case vsBuildAction.vsBuildActionRebuildAll:
                            System.Windows.MessageBox.Show($"{Scope.ToString()}/{Action.ToString()}");
                            break;
                        case vsBuildAction.vsBuildActionClean:
                            System.Windows.MessageBox.Show($"{Scope.ToString()}/{Action.ToString()}");
                            break;
                        case vsBuildAction.vsBuildActionDeploy:
                            System.Windows.MessageBox.Show($"{Scope.ToString()}/{Action.ToString()}");
                            break;
                        default:
                            break;
                    }
                }

            }
            catch (System.Exception)
            {

                throw;

            }
            finally
            {

                _Service.Events.BuildEvents.OnBuildDone -= _BuildDone;

            }

        }

    }
}
