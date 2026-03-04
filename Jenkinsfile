// Jenkinsfile for Console Application

pipeline {
    agent any
    
    environment {
        PROJECT_NAME = "BankingApp"
        SOLUTION_FILE = "BankingApp.sln"
        OUTPUT_DIR = "${WORKSPACE}\\publish"
        DEPLOYMENT_DIR = "C:\\Deployments\\${PROJECT_NAME}"
    }
    
    triggers {
        pollSCM('H/5 * * * *')
    }
    
    stages {
        stage('Checkout') {
            steps {
                echo 'Checking out code...'
                checkout scm
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
                bat """
                    dotnet publish ${SOLUTION_FILE} ^
                        --configuration Release ^
                        --output ${OUTPUT_DIR} ^
                        --no-build
                """
            }
        }
        
        stage('Deploy') {
            steps {
                echo 'Deploying to local directory...'
                bat """
                    if not exist "${DEPLOYMENT_DIR}" mkdir "${DEPLOYMENT_DIR}"
                    xcopy /E /I /Y "${OUTPUT_DIR}" "${DEPLOYMENT_DIR}"
                """
            }
        }
        
        stage('Verify') {
            steps {
                echo 'Verifying deployment...'
                bat """
                    if exist "${DEPLOYMENT_DIR}\\${PROJECT_NAME}.dll" (
                        echo Deployment successful!
                    ) else (
                        echo Deployment failed!
                        exit 1
                    )
                """
            }
        }
    }
    
    post {
        success {
            echo 'Build completed successfully!'
            archiveArtifacts artifacts: 'publish/**/*', allowEmptyArchive: true
        }
        failure {
            echo 'Build failed!'
        }
        always {
            cleanWs()
        }
    }
}