import { nodeResolve } from '@rollup/plugin-node-resolve';
import { babel } from '@rollup/plugin-babel';

export default {
	input: "build/fs/main.js",
	output: {
		inlineDynamicImports: true,
		file: 'build/dist/main.js',
		format: 'cjs'
	},
	plugins: [
		nodeResolve(),
		babel({ babelHelpers: 'bundled' })
	]
};
