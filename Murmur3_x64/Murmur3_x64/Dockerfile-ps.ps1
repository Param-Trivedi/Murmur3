<#
.SYNOPSIS
Builds and runs a Docker image.

.PARAMETER Run
Runs docker run murmur3_x64 

.PARAMETER Build
Builds a Docker image.

.EXAMPLE
C:\PS> .\Dockerfile-ps.ps1 -Build 
C:\PS> .\Dockerfile-ps.ps1 -Run 

Build a Docker image named murmur3_x64
#>

Param(
    [Parameter(Mandatory=$True,ParameterSetName="Run")]
    [switch]$Run,
    [Parameter(Mandatory=$True,ParameterSetName="Build")]
    [switch]$Build,
    [parameter(ParameterSetName="Run")]
    [parameter(ParameterSetName="Build")]
	[ValidateNotNullOrEmpty()]
    [string]$Config="release" ,
    [string]$Version="latest"
	)


# Builds the Docker image.
function BuildImage () {
   
    #To build the image
	docker build -t murmur3_x64 .
	
	# This is to remove untagged images created as a result of multi-stage build
	docker rmi $(docker images -f 'dangling=true' -q) -f

}

#Runs the Docker image.
function RunImage () {
    #Run the image
	docker run murmur3_x64
}

# Call the correct function for the parameter that was used
if($Run) {
    RunImage
}
elseif($Build) {
    BuildImage
}
