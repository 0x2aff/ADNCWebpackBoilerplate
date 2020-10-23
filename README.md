
# ADNCWebpackBoilerplate
ASP.NET Core with RazorPages, EFCore, Webpack, Babel, SASS and Bootstrap in Docker Boilerplate.

> Entrypoint for new projects that i created because I was missing Webpack bundling with babel in ASP.NET Core. Feel free to use this as you please. I'm always happy about contributions / feedback for this. 
> 
> Love you all :heart:

## Installation
**1.Clone this repository**

    git clone https://github.com/0x2aff/ADNCWebpackBoilerplate.git
    
**2.Install NPM packages**

    cd ADNCWebpackBoilerplate && npm install

**3.Open the ADNCWebpackBoilerplate.sln**

## Webpack Usage

### Script Commands
***Build***
Runs the webpack bundler in production mode. This will bundle the sass and js into "wwwroot/dist/".

    npm run build

***Watch***
Runs the webpack bundler in watch and development mode. This will bundle the sass and js into "wwwroot/dist/" everytime a sass or js file in "./Client/[Scripts|Stylesheets]" is changed.

    npm run watch

### Visual Studio Task Runner
I added a "-vs-binding" for the Visual Studio Task Runner into the "package.json" that will automatically start the watch script command as soon as the project gets loaded into Visual Studio.
You can download the Visual Studio Task Runner [here]((https://marketplace.visualstudio.com/items?itemName=MadsKristensen.TaskRunnerExplorer)).

## Infos
Config.json contains a simple configuration fields for MariaDB in EFCore. Just check the Startup.cs for everything else. Feel free to open an issue if you have questions.
