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
                bat 'dotnet build AstraLingo.sln -c Release'
            }
        }

        stage('Publish') {
            steps {
                // IMPORTANT FIX: publish INSIDE Docker context
                bat 'dotnet publish AstraLingo/AstraLingo.csproj -c Release -o AstraLingo/publish'
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
                docker run -d -p 9090:8080 --name astralingo astralingo:v2
                '''
            }
        }

        stage('Test WSL') {
            steps {
                bat 'wsl echo hello'
            }
        }

        stage('Ansible Deploy') {
            steps {
                bat '''
                wsl bash -lc "
                cd /mnt/c/Users/Muskan/ansible &&
                ansible-playbook -i inventory.ini deploy.yml
                "
                '''
            }
        }
    }

    post {
        success {
            echo 'PIPELINE SUCCESS '
        }
        failure {
            echo 'PIPELINE FAILED '
        }
    }
}
