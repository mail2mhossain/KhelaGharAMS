param($installPath, $toolsPath, $package, $project)

$controllers = $project.ProjectItems | Where-Object { $_.Name -eq "Controllers" } 
$accountController = $controllers.ProjectItems | Where-Object { $_.Name -eq "AccountController.cs" } 

if ($accountController) {
	$accountController.Name = $accountController.Name + ".old" 
}

$ph = $project.ProjectItems | Where-Object { $_.Name -eq "mvc-pre.txt" }

if ($ph){
	$ph.Delete()
} 



