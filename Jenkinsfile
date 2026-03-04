pipeline {
    agent any
    
    environment {
        PROJECT_NAME = "BankingApp"
        SOLUTION_FILE = "BankingApp.sln"
        OUTPUT_DIR = "${WORKSPACE}\\publish"
        DEPLOYMENT_DIR = "C:\\Deployments\\${PROJECT_NAME}"
    }
    
    stages {
        stage('Checkout') {
            steps {
                echo 'Checking out code...'
                checkout scm
                bat 'dotnet --version'
            }
        }
        
        stage('Restore') {
            steps {
                echo 'Restoring NuGet packages...'
                bat "dotnet restore ${SOLUTION_FILE}"
            }
        }
        
        stage('Build') {
            steps {
                echo 'Building solution...'
                bat "dotnet build ${SOLUTION_FILE} --configuration Release --no-restore"
            }
        }
        
        stage('Publish') {
            steps {
                echo 'Publishing application...'
                bat "dotnet publish ${SOLUTION_FILE} --configuration Release --output ${OUTPUT_DIR} --no-build"
            }
        }
        
        stage('Deploy') {
            steps {
                echo 'Deploying to local directory...'
                bat """
                    if not exist "${DEPLOYMENT_DIR}" mkdir "${DEPLOYMENT_DIR}"
                    xcopy /E /I /Y "${OUTPUT_DIR}" "${DEPLOYMENT_DIR}"
                    dir "${DEPLOYMENT_DIR}"
                """
            }
        }
    }
    
    post {
        success {
            echo 'BUILD SUCCESSFUL!'
            echo "Deployed to: ${DEPLOYMENT_DIR}"
        }
        failure {
            echo 'BUILD FAILED!'
        }
    }
}