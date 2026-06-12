pipeline {
    agent any

    stages {

        stage('Checkout') {
            steps {
                git url: 'https://github.com/Muskan-02-786/AstraLingo.git', branch: 'master'
            }
        }

        stage('Build') {
            steps {
                bat 'dotnet build AstraLingo.sln -c Release'
            }
        }

        stage('Publish') {
            steps {
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

        stage('Ansible Deploy (WSL)') {
            steps {
                bat '''
                wsl bash -lc "
                cd ~/ansible &&
                ansible-playbook -i inventory.ini deploy.yml
                "
                '''
            }
        }
    }

    post {
        success {
            echo 'SUCCESS: Deployment done'
        }
        failure {
            echo 'FAILED pipeline'
        }
    }
}
