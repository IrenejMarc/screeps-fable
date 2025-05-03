module.exports = function(grunt) {
	const host = process.env.SCREEPS_HOST;
	const port = Number(process.env.SCREEPS_PORT);
	const http = process.env.SCREEPS_USE_HTTP == "y"

	const email = process.env.SCREEPS_EMAIL;
	const password = process.env.SCREEPS_PASSWORD;
	const branch = process.env.SCREEPS_BRANCH;
	const ptr = process.env.SCREEPS_PTR == "y";

	grunt.loadNpmTasks('grunt-screeps');

	grunt.initConfig({
		screeps: {
			options: {
				server: {
					host,
					port,
					http,
				},
				email,
				password,
				branch,
				ptr,
			},
			dist: {
				src: ['build/dist/**/*.js', 'build/dist/**/*.js.map']
			}
		}
	});
}
