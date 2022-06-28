@Library('smsoft-libs')_
pipeline	{
   options {
        buildDiscarder logRotator(numToKeepStr: '3')
        disableConcurrentBuilds()
    }
    agent any
	stages	{
        stage('deploy queue') {
            when {
                branch "develop"
            }
            steps {
				deployQueueArtemis()
            }
        }
    }
}
