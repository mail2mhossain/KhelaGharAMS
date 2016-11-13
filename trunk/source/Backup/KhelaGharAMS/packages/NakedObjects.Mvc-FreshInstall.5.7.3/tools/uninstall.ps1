param($installPath, $toolsPath, $package, $project)


$views = $project.ProjectItems | Where-Object { $_.Name -eq "Views" }
$sharedViews = $views.ProjectItems | Where-Object { $_.Name -eq "Shared" }
$masterLogin = $sharedViews.ProjectItems | Where-Object { $_.Name -eq "Site.WithServices.Master" } 
$masterNoLogin = $sharedViews.ProjectItems | Where-Object { $_.Name -eq "Site.WithServices.Nologin.Master" } 
$masterNoLoginNoBundle = $sharedViews.ProjectItems | Where-Object { $_.Name -eq "Site.WithServices.Nologin.Nobundle.Master" } 


if ($masterLogin) {
	
	$masterLogin.Delete()
}

if ($masterNoLogin ) {
	
	$masterNoLogin .Delete()
}

if ($masterNoLoginNoBundle) {
	
	$masterNoLoginNoBundle.Delete()
}



