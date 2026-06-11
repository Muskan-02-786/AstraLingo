pipeline {

    agent any

    stages {

        stage('Checkout') {

            steps {

                git branch: 'master',
                url: 'https://github.com/Muskan-02-786/AstraLingo.git'
            }
        }

        stage('Restore') {

            steps {

                bat 'dotnet restore AstraLingo.sln'
            }
        }

        stage('Build') {

            steps {

                bat 'dotnet build AstraLingo.sln --configuration Release'
            }
        }

        stage('Publish') {

            steps {

                bat 'dotnet publish AstraLingo/AstraLingo.csproj -c Release -o publish'
            }
        }
    }
}