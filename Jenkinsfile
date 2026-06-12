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
                bat '''
                cd AstraLingo && docker build -t astralingo:v2 .
                '''
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

        // ✅ TEST WSL (YOU ASKED FOR THIS)
        stage('Test WSL') {
            steps {
                bat 'wsl echo hello'
            }
        }

        stage('Ansible Deploy') {
            steps {
                bat '''
                wsl bash -lc "
                cd /mnt/d/MUSKAN/AstraLingo/AstraLingo/AstraLingo &&
                ansible-playbook -i ansible/inventory.ini ansible/deploy.yml
                "
                '''
            }
        }
    }

    post {
        success {
            echo 'Deployment Successful'
        }
        failure {
            echo 'Deployment Failed'
        }
    }
}
