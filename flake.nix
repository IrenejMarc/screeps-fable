{
  description = "Testing for Screeps because why not";

  inputs.nixpkgs.url = "github:NixOS/nixpkgs/nixpkgs-unstable";

  outputs = { self, nixpkgs, flake-utils }@inputs: {
  } // flake-utils.lib.eachDefaultSystem (system: let
    pkgs = nixpkgs.legacyPackages.${system};
  in {
    devShells.default = import ./nix/shell.nix { inherit self pkgs inputs; };
  });
}
