param($installPath, $toolsPath, $package, $project)
	
	$nakedObjectsFolder = "Naked Objects"
	$CSSnippets = "$toolsPath\C#\*.snippet"

	$CSTemplates = "$toolsPath\C#\*.zip"
	$vsVersions = @("2012","2010")

	Function copyFiles ($toParent, $toDir,  $files) {

		if (Test-Path $toParent) {
			$destination = "$toParent$toDir"
			
			if (!(Test-Path $destination)) {			
				New-Item $destination -itemType "directory"	
			}	

			"Installing to $destination for Visual Studio $vsVersion"
			Copy-Item $files $destination			
		}		
	}


	Foreach ($vsVersion in $vsVersions) {
		
		$docRoot = [System.Environment]::GetFolderPath("MyDocuments")
		$CSSnippetsFolder = "$docRoot\Visual Studio $vsVersion\Code Snippets\Visual C#\My Code Snippets\"
		$CSItemsFolder = "$docRoot\Visual Studio $vsVersion\Templates\ItemTemplates\Visual C#\"
		copyFiles $CSSnippetsFolder $nakedObjectsFolder $CSSnippets
	    copyFiles $CSItemsFolder $nakedObjectsFolder $CSTemplates
	}