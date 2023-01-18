using UnityEngine;
using UnityEngine.UI;

public class HLVideoRewardTest : MonoBehaviour
{
    [SerializeField]
    private Button btAds;
    [SerializeField]
    private Text lblReward;
    [SerializeField]
    private Image imgVideo;

	private HLIronSourceVideoRequestService admobMediatorVideo;
    private bool isAds;
    private static int rewardCount = 0;
    private bool isReward;

	private void OnDestroy()
	{
		HLIronSourceAdapter.OnVideoLoad -= OnVideoLoaded;
        HLIronSourceListener.OnVideoComplete -= OnVideoComplete;
        HLIronSourceListener.OnVideoFailed -= OnVideoFailed;
    }

    private void Start()
    {
        admobMediatorVideo = GameObject.FindObjectOfType<HLIronSourceVideoRequestService>();

        HLIronSourceAdapter.OnVideoLoad += OnVideoLoaded;
        HLIronSourceListener.OnVideoComplete += OnVideoComplete;
        HLIronSourceListener.OnVideoFailed += OnVideoFailed;

        isAds = admobMediatorVideo.isVideoReward;
        RefreshAdsStatus();
        if(rewardCount > 0)
        {
            lblReward.text = "Reward " + rewardCount;
        }
    }

    private void OnVideoLoaded(bool isLoad)
	{
        isAds = isLoad;
        RefreshAdsStatus();
	}

    private void OnVideoComplete()
    {
        rewardCount++;
        lblReward.text = "Reward "+rewardCount;
        RefreshAdsStatus();
    }

    private void OnVideoFailed()
    {
        lblReward.text = "Failed Reward";
        RefreshAdsStatus();
    }

    public void DoPlayAds()
    {
        isAds = admobMediatorVideo.isVideoReward;
        if (isAds)
        {
            isAds = false;
            lblReward.text = "";
            admobMediatorVideo.ShowRewardedVideoAds();
        }
        RefreshAdsStatus();
    }

    private void RefreshAdsStatus()
    {
        if (isReward)
        {
            imgVideo.gameObject.SetActive(false);
            btAds.interactable = false;
        }
        else
        {
            btAds.interactable = isAds;
            imgVideo.gameObject.SetActive(true);
            imgVideo.color = isAds ? Color.green : Color.gray;
        }
    }
}
