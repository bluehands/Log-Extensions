# Copyright (c) 2004-2016 Jaroslaw Kowalski <jaak@jkowalski.net>, Kim Christensen, Julian Verdurmen

# All rights reserved.

param($installPath, $toolsPath, $package, $project)

$configItem = $project.ProjectItems.Item("NLog.config")

# set 'Copy To Output Directory' to 'Copy if newer'
$copyToOutput = $configItem.Properties.Item("CopyToOutputDirectory")
$copyToOutput.Value = 1

# set 'Build Action' to 'Content'
$buildAction = $configItem.Properties.Item("BuildAction")
$buildAction.Value = 2
