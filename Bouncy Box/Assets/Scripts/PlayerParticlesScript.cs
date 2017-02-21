using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerParticlesScript : MonoBehaviour {

	[SerializeField] ParticleSystem playerParticleSystem;
	bool isDead;
	float deathTimer;
	
	void Start () {
		isDead = false;
		deathTimer = .3f;
	}
	
	// Update is called once per frame
	void Update () {
		if (isDead) {
			deathTimer -= Time.deltaTime;
		}
		if (deathTimer <= 0) {
			
			SceneManager.LoadScene (SceneManager.GetActiveScene().name);
		}
	}

	public void JumpParticle () {
		var ma = playerParticleSystem.main;
		var col = playerParticleSystem.colorOverLifetime;
		col.enabled = true;
		ma.startColor = Color.green;
		Gradient grad = new Gradient ();
		grad.SetKeys (new GradientColorKey[] {  }, new GradientAlphaKey[] {
			new GradientAlphaKey (1.0f, 0.0f),
			new GradientAlphaKey (0.0f, 1f)
		});
		col.color = grad;
		ma.startLifetime = .3f;
		playerParticleSystem.Emit (10);
	}

	public void yellowBoxParticle () {
		var ma = playerParticleSystem.main;
		var col = playerParticleSystem.colorOverLifetime;
		col.enabled = true;
		ma.startColor = Color.yellow;
		Gradient grad = new Gradient ();
		grad.SetKeys (new GradientColorKey[] {  }, new GradientAlphaKey[] {
			new GradientAlphaKey (1.0f, 0.0f),
			new GradientAlphaKey (0.0f, 1f)
		});
		col.color = grad;
		ma.startLifetime = .3f;
		playerParticleSystem.Emit (10);
	}

	public void deathParticles () {
		var ma = playerParticleSystem.main;
		var col = playerParticleSystem.colorOverLifetime;
		Color[] redGreen = new Color[] { Color.red, Color.green};
		col.enabled = true;
		Gradient grad = new Gradient ();
		grad.SetKeys (new GradientColorKey[] {
		}, new GradientAlphaKey[] {
			new GradientAlphaKey (1.0f, 0.0f),
			new GradientAlphaKey (0.0f, 1f)
		});
		ma.startLifetime = .3f;
		for (int i = 0; i < 20; i++) {
			ma.startColor = redGreen[Random.Range (0, redGreen.Length)];
			playerParticleSystem.Emit (1);
		}
		isDead = true;
	}
}
