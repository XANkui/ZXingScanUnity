using System;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TBEasyWebCam;

public class QRDecodeTest : MonoBehaviour
{
	public QRCodeDecodeController e_qrController;

	public Text UiRightText;
	public Text UiLeftText;
	public GameObject dataInfoLeft;
	public GameObject dataInfoRight;

	public GameObject resetBtn;

	public GameObject scanLineObj;

	bool isTorchOn = false;

	public Sprite torchOnSprite;
	public Sprite torchOffSprite;
	public Image torchImage;
	/// <summary>
	/// when you set the var is true,if the result of the decode is web url,it will open with browser.
	/// </summary>
	public bool isOpenBrowserIfUrl;

	private void Start()
	{
		if (this.e_qrController != null)
		{
			this.e_qrController.onQRScanFinished += new QRCodeDecodeController.QRScanFinished(this.qrScanFinished);
		}
	}

	private void Update()
	{
	}

	private void qrScanFinished(string dataText)
	{
		if (isOpenBrowserIfUrl) {
			if (Utility.CheckIsUrlFormat(dataText))
			{
				if (!dataText.Contains("http://") && !dataText.Contains("https://"))
				{
					dataText = "http://" + dataText;
				}
				Application.OpenURL(dataText);
			}
		}

		//二维码形式
		if (dataText.Contains ("http://")) {
			this.UiLeftText.text = dataText;
			this.dataInfoLeft.gameObject.SetActive (true);
		} else {	//条形码形式
			
			this.UiRightText.text = dataText;
			this.dataInfoRight.gameObject.SetActive (true);
		}


		if (this.resetBtn != null)
		{
			this.resetBtn.SetActive(true);
		}
		if (this.scanLineObj != null)
		{
			this.scanLineObj.SetActive(false);
		}

	}

	public void Reset()
	{
		if (this.e_qrController != null)
		{
			this.e_qrController.Reset();
		}
		if (this.UiRightText != null)
		{
			this.UiRightText.text = string.Empty;
			//避免关闭的信息显示框是打开的
			this.UiRightText.gameObject.SetActive (true);

			//先隐藏扫描出结果后显示
			this.dataInfoLeft.gameObject.SetActive (false);
			this.dataInfoRight.gameObject.SetActive (false);
		}

		if (this.UiLeftText != null)
		{
			this.UiLeftText.text = string.Empty;
			//避免关闭的信息显示框是打开的
			this.UiLeftText.gameObject.SetActive (true);

			//先隐藏扫描出结果后显示
			this.dataInfoLeft.gameObject.SetActive (false);
			this.dataInfoRight.gameObject.SetActive (false);
		}
		if (this.resetBtn != null)
		{
			this.resetBtn.SetActive(false);
		}
		if (this.scanLineObj != null)
		{
			this.scanLineObj.SetActive(true);
		}
	}

	public void Play()
	{
		Reset ();
		if (this.e_qrController != null)
		{
			this.e_qrController.StartWork();
		}
	}

	public void Stop()
	{
		if (this.e_qrController != null)
		{
			this.e_qrController.StopWork();
		}

		if (this.resetBtn != null)
		{
			this.resetBtn.SetActive(false);
		}
		if (this.scanLineObj != null)
		{
			this.scanLineObj.SetActive(false);
		}

		//关闭扫描信息显示框
		if( this.UiRightText.gameObject != null) {
			this.UiRightText.gameObject.SetActive (false);
		}

		//关闭扫描信息显示框
		if( this.UiLeftText.gameObject != null) {
			this.UiLeftText.gameObject.SetActive (false);
		}

		//先隐藏扫描出结果后显示
		this.dataInfoLeft.gameObject.SetActive (false);
		this.dataInfoRight.gameObject.SetActive (false);
	}

	public void GotoNextScene(string scenename)
	{
		if (this.e_qrController != null)
		{
			this.e_qrController.StopWork();
		}
		//Application.LoadLevel(scenename);
		SceneManager.LoadScene(scenename);
	}

	/// <summary>
	/// Toggles the torch by click the ui button
	/// note: support the feature by using the EasyWebCam Component 
	/// </summary>
	public void toggleTorch()
	{
		#if UNITY_ANDROID && !UNITY_EDITOR
		if (EasyWebCam.isActive) {
			if (isTorchOn) {
				torchImage.sprite = torchOffSprite;
				EasyWebCam.setTorchMode (TBEasyWebCam.Setting.TorchMode.Off);
			} else {
				torchImage.sprite = torchOnSprite;
				EasyWebCam.setTorchMode (TBEasyWebCam.Setting.TorchMode.On);
			}
			isTorchOn = !isTorchOn;
		}
		#endif
	}

}
