
function Update () {
	if(transform.parent){
		renderer.material.SetColor("_TintColor", transform.parent.renderer.material.GetColor("_TintColor"));
	}
}