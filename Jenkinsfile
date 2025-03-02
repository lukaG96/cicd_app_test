pipeline {
    agent {
        label 'dotnet_slave' // Change this to match your Jenkins agent label
    }
    
    environment {
        DOTNET_VERSION = '8.0'
        SOLUTION_NAME = 'cicd_app_test.sln' // Change this to your actual solution file
    }

    stages {
        stage('Checkout') {
            steps {
                git branch: 'main', url: 'https://github.com/lukaG96/cicd_app_test.git' // Change to your repo
            }
        }

        stage('Install .NET SDK') {
            steps {
                script {
                    sh 'wget https://dot.net/v1/dotnet-install.sh -O dotnet-install.sh'
                    sh 'chmod +x dotnet-install.sh'
                    sh './dotnet-install.sh --version $DOTNET_VERSION'
                    sh 'export PATH=$HOME/.dotnet:$PATH'
                }
            }
        }

        stage('Restore Dependencies') {
            steps {
                sh 'dotnet restore $SOLUTION_NAME'
            }
        }

        stage('Build') {
            steps {
                sh 'dotnet build $SOLUTION_NAME --configuration Release --no-restore'
            }
        }

        stage('Run Tests') {
            steps {
                sh 'dotnet test $SOLUTION_NAME --configuration Release --no-build --logger trx'
            }
        }

        stage('Publish') {
            steps {
                sh 'dotnet publish $SOLUTION_NAME -c Release -o publish_output'
            }
        }

        stage('Archive Artifacts') {
            steps {
                archiveArtifacts artifacts: 'publish_output/**', fingerprint: true
            }
        }
    }

    // post {
    //     always {
    //         junit '**/TestResults/*.trx' // Publish test results
    //     }
    // }
}
