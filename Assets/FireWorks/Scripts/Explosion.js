var glow : Renderer;
var explosionColors : Color[];
var trail : Transform;
var trails : int;
var trailVelocity : float;
var useTrailColor = true;
var isBasicExplosion = true;
var explosionDetail = false;
var crackling = false;
var cracklingEffect : Transform;
function Start () {
	if(audio){
		audio.pitch = Random.Range(.8, 1.2);
	}
	var randomMaterial = explosionColors[Random.value*explosionColors.length];
	if(glow){
			glow.material.SetColor("_TintColor", Color(randomMaterial.r, randomMaterial.g, randomMaterial.b, 0.1));
	}
	if(explosionDetail){
		renderer.material.SetColor("_TintColor", randomMaterial);
	}

	if(isBasicExplosion == true){
		renderer.material.SetColor("_TintColor", randomMaterial);
	}
	else{
		for(i = 0; i < trails; i++){
			var copy = Instantiate(trail,transform.position, transform.rotation);
			copy.transform.parent = gameObject.Find("RocketLauncher").transform;
			if(useTrailColor){
				copy.gameObject.GetComponent("Gravity").col = randomMaterial;
			}
			var randomx = Random.Range(-trailVelocity, trailVelocity);
			var randomy = Random.Range(-trailVelocity, trailVelocity);
			var randomz = Random.Range(-trailVelocity, trailVelocity);
			copy.rigidbody.velocity = Vector3(randomx, randomy ,randomz);
		}
	}
	if(crackling){
		yield WaitForSeconds(.65);
		var crackle = Instantiate(cracklingEffect,transform.position, transform.rotation);
		crackle.renderer.material.SetColor("_TintColor", renderer.material.GetColor("_TintColor"));
	}
	
}