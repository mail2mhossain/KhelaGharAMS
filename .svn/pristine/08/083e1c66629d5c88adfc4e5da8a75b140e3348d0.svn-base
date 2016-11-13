param($installPath, $toolsPath, $package, $project)

function CountSolutionFilesByExtension($project, $extension) {
	$files = $project.DTE.Solution `
		| ?{ $_.FileName } `
		| %{ [System.IO.Path]::GetDirectoryName($_.FileName) } `
		| %{ [System.IO.Directory]::EnumerateFiles($_, "*." + $extension, [System.IO.SearchOption]::AllDirectories) }
	($files | Measure-Object).Count
}

if ((CountSolutionFilesByExtension $project csproj) -eq 0) { throw "Must be installed in a C# project" }

if ((CountSolutionFilesByExtension $project asax) -eq 0) { throw "No Global.asax found - project must be MVC (3 or greater)" }

$systemwebmvcdll =  $project.object.references | where-object {$_.Name -eq "System.Web.MVC"}

if ($systemwebmvcdll) {

	if ($systemwebmvcdll.Version -eq "4.0.0.0"){
		"project is MVC 4"
	}
	else {
		throw "Project must be MVC 4"
	}
}
else {
	throw "Project must be MVC 4"	
}

$controllers = $project.ProjectItems | Where-Object { $_.Name -eq "Controllers" } 
$views = $project.ProjectItems | Where-Object { $_.Name -eq "Views" }
$appstart = $project.ProjectItems | Where-Object { $_.Name -eq "App_Start" }
$sharedViews = $views.ProjectItems | Where-Object { $_.Name -eq "Shared" }
$accountControllerOld = $controllers.ProjectItems | Where-Object { $_.Name -eq "AccountController.cs.old" } 
$accountControllerNew = $controllers.ProjectItems | Where-Object { $_.Name -eq "AccountController.cs" }
$masterLogin = $sharedViews.ProjectItems | Where-Object { $_.Name -eq "Site.WithServices.Master" } 
$masterNoLogin = $sharedViews.ProjectItems | Where-Object { $_.Name -eq "Site.WithServices.Nologin.Master" } 
$masterNoLoginNoBundle = $sharedViews.ProjectItems | Where-Object { $_.Name -eq "Site.WithServices.Nologin.Nobundle.Master" } 
$bundleConfig = $appstart.ProjectItems | Where-Object { $_.Name -eq "BundleConfig.cs" }

if ($bundleConfig){
	# we have bundle config so remove no bundle master
	$masterNoLoginNoBundle.Delete()
}
else{
	# no bundle config so remove bundle master and copy in no bundle master 
	$masterNoLogin.Delete()
	$masterNoLogin = $masterNoLoginNoBundle
}

if ($accountControllerOld) {
	# remove old account controller and leave new one 
	$accountControllerOld.Delete()

	# remove nologon master page 
	$masterNoLogin.Delete()
}
else {
	# remove new account controller as no account controller was installed originally 
	$accountControllerNew.Delete()

	# remove login master page and rename login one 
	$masterLogin.Delete()
	$masterNoLogin.Name = "Site.WithServices.Master"
}


