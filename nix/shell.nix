{ self, pkgs, inputs, ... }: let
	fablePkg = pkgs.fable;

	mkAlias = name: cmd: pkgs.writeShellScriptBin name cmd;

	gruntCmd = (mkAlias "grunt" ''
		${pkgs.nodePackages.grunt-cli}/bin/grunt $@
	'');

	rollupCmd = (mkAlias "rollup" ''
		${pkgs.nodejs}/bin/node ./node_modules/rollup/dist/bin/rollup $@
	'');

	pushCmd = (mkAlias "push" ''
		${pkgs.nodePackages.grunt-cli}/bin/grunt screeps
	'');

	cleanCmd = (mkAlias "clean" ''
		mkdir -p ./build/fs ./build/dist
		rm -rf ./build/fs/* ./build/dist/*
	'');

	buildCmd = (mkAlias "build" ''
		${pkgs.fable}/bin/fable \
			--cwd . \
			--sourceMaps \
			--outDir ./build/fs \
			--language js \
			--extension .js \
			$@
  '');

	rollupPushCmd = (mkAlias "rollup_and_push" ''
		${rollupCmd}/bin/rollup -c
		${pushCmd}/bin/push
	'');

	devCmd = (mkAlias "dev" ''
		${buildCmd}/bin/build --watch --run ${rollupPushCmd}/bin/rollup_and_push
	'');
in
	pkgs.mkShell {
		packages = with pkgs; [
			dotnet-sdk
			fsautocomplete
			fablePkg

			nodejs
			nodePackages.yarn

			pushCmd
			buildCmd
			cleanCmd
			gruntCmd
			rollupCmd
			rollupPushCmd
			devCmd
		];
	}
