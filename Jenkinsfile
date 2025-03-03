pipeline {
    agent {
        label 'dotnet_slave' // Ensure this label matches your Jenkins agent that has Docker installed
    }
    
    environment {
        DOTNET_VERSION = '8.0.406'  // Set to .NET 8.0 version
        SOLUTION_NAME = 'cicd_app_test.sln' // Change this to your actual solution file
        IMAGE_NAME = 'cicd_app_test'  // Docker image name
        IMAGE_VERSION = 'latest'  // Set Docker image version (you can modify as needed)
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
                    // Download .NET 8.0 SDK installer
                    sh 'wget https://download.visualstudio.microsoft.com/download/pr/d2abdb4c-a96e-4123-9351-e4dd2ea20905/e8010ae2688786ffc1ebca4ebb52f41b/dotnet-sdk-8.0.406-linux-x64.tar.gz -O dotnet-sdk-8.0.tar.gz'
                    
                    // Install .NET SDK
                    sh 'mkdir -p $HOME/.dotnet'
                    sh 'tar -xvf dotnet-sdk-8.0.tar.gz -C $HOME/.dotnet'
                    
                    // Update PATH to include .NET SDK
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

        stage("Docker Build") {
            steps {
                script {
                    echo '<--------------- Docker Build Started --------------->'
                    // Ensure the Docker Pipeline plugin is available by using docker.build() inside the script block
                    def app = docker.build("${IMAGE_NAME}:${IMAGE_VERSION}", "--file Dockerfile .")
                    echo '<--------------- Docker Build Completed --------------->'
                }
            }
        }

        stage('Archive Artifacts') {
            steps {
                archiveArtifacts artifacts: 'publish_output/**', fingerprint: true
            }
        }
    }

    // Uncomment this if you want to publish test results
    // post {
    //     always {
    //         junit '**/TestResults/*.trx' // Publish test results
    //     }
    // }
}
