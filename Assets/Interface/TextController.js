#pragma strict
var IsShown : boolean = true;
var timeToShow : float = 5.0f;
var Text : Texture;


function Update () {
	if(IsShown){
		timeToShow -= Time.deltaTime;
		if(timeToShow < 0f)
			IsShown = false;
	}
}

function OnGUI(){
	if(IsShown)
		GUI.DrawTexture(Rect(0,0,200,200), Text, ScaleMode.ScaleToFit,true, 0f);
	else 
		Text = null;
		
}

