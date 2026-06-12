pipeline {
    agent any

    stages {

        stage('Checkout') {
            steps {
                git url: 'https://github.com/Muskan-02-786/AstraLingo.git', branch: 'master'
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

        stage('Docker Build') {
            steps {
                bat 'docker build -t astralingo:v2 AstraLingo'
            }
        }

        stage('Run Container') {
            steps {
                bat '''
                docker rm -f astralingo || exit 0
                docker run -d -p 9090:80 --name astralingo astralingo:v2
                '''
            }
        }
    }

    post {
        success {
            echo 'CI/CD SUCCESS '
        }
        failure {
            echo 'CI/CD FAILED '
        }
    }
}
