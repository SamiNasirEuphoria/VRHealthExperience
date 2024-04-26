using UnityEngine;
using RenderHeads.Media.AVProVideo;
using BNG;
using System.Collections;
using UnityEngine.Events;
public class VideoSceneManager : MonoBehaviour
{
    public MediaPlayer myPlayer;
    string basicUrl;
    private void Start()
    {
        basicUrl = "/Users/mac/Documents/";

        /// Users / mac / Documents / OH_Video_3.mp4
        myPlayer.OpenMedia(new MediaPath(basicUrl+"OH_Video_3.mp4", MediaPathType.AbsolutePathOrURL), autoPlay: true);
        //myPlayer.Play();
       
    }
}
