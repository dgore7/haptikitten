package com.example.augustk.tanvasapp;

/**
 * Created by augustk on 11/19/2016.
 */

import android.content.Context;
import android.util.Log;
import android.view.MotionEvent;

import org.rajawali3d.lights.DirectionalLight;
import org.rajawali3d.materials.Material;
import org.rajawali3d.materials.methods.DiffuseMethod;
import org.rajawali3d.materials.textures.ATexture;
import org.rajawali3d.materials.textures.Texture;
import org.rajawali3d.math.vector.Vector3;
import org.rajawali3d.primitives.Sphere;
import org.rajawali3d.renderer.Renderer;

public class CustomRenderer extends Renderer {

    private Sphere mEarthSphere;
    private DirectionalLight mDirectionalLight;

    public CustomRenderer(Context context) {
        super(context);
        //this.context = context;
        setFrameRate(60);
    }

    @Override
    public void initScene() {
        mDirectionalLight = new DirectionalLight(1f, .2f, -1.0f);
        mDirectionalLight.setColor(1.0f, 1.0f, 1.0f);
        mDirectionalLight.setPower(2);
        getCurrentScene().addLight(mDirectionalLight);

        Material material = new Material();
        material.enableLighting(true);
        material.setDiffuseMethod(new DiffuseMethod.Lambert());
        material.setColorInfluence(0);
        Texture earthTexture = new Texture("Earth", R.drawable.noise_texture);
        try {
            material.addTexture(earthTexture);

        } catch (ATexture.TextureException error){
            Log.d("DEBUG", "TEXTURE ERROR");
        }


        mEarthSphere = new Sphere(1, 24, 24);
        mEarthSphere.setMaterial(material);
        getCurrentScene().addChild(mEarthSphere);
        getCurrentCamera().setZ(4.2f);
    }

    @Override
    public void onTouchEvent(MotionEvent event){
    }

    @Override
    public void onOffsetsChanged(float x, float y, float z, float w, int i, int j){
    }

    @Override
    public void onRender(final long elapsedTime, final double deltaTime) {
        super.onRender(elapsedTime, deltaTime);
        mEarthSphere.rotate(Vector3.Axis.Y, 1.0);
    }
}
