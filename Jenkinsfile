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

        stage('Docker Build') {
            steps {
                bat 'cd AstraLingo && docker build --pull=false -t astralingo:v2 .'
            }
        }

        stage('Run Container') {
            steps {
                bat 'docker rm -f astralingo || exit 0'
                bat 'docker run -d -p 9090:80 --name astralingo astralingo:v2'
            }
        }

        stage('Ansible Deploy') {
            steps {
                bat 'wsl bash -c "cd /mnt/d/MUSKAN/AstraLingo/AstraLingo/AstraLingo && ansible-playbook -i ansible/inventory.ini ansible/deploy.yml"'
            }
        }
    }

    post {
        success {
            echo 'Deployment Successful '
        }
        failure {
            echo 'Deployment Failed '
        }
    }
}
