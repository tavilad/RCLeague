using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomLayoutGroup : MonoBehaviour
{


	[SerializeField] private GameObject _roomListingPrefab;

	public GameObject RoomListingPrefab
	{
		get { return _roomListingPrefab; }
	}
	
	
	private List<RoomListing> _roomListingsButtons=new List<RoomListing>();

	public List<RoomListing> RoomListingsButtons
	{
		get { return _roomListingsButtons; }
	}

	private void OnReceivedRoomListUpdate()
	{
		RoomInfo[] rooms = PhotonNetwork.GetRoomList();

		foreach (var room in rooms)
		{
		
			RoomReceived(room);
			
		}
		
		RemoveOldRooms();
	}

	private void RoomReceived(RoomInfo room)
	{
		int index = RoomListingsButtons.FindIndex(x => x.RoomName == room.Name);

		if (index == -1)
		{
			if (room.IsVisible && room.PlayerCount < room.MaxPlayers)
			{
				GameObject roomListingObj = Instantiate(RoomListingPrefab);
				roomListingObj.transform.SetParent(transform,false);

				RoomListing roomListing = roomListingObj.GetComponent<RoomListing>();
				RoomListingsButtons.Add(roomListing);

				index = RoomListingsButtons.Count - 1;

			}
		}

		if (index != -1)
		{
			RoomListing roomListing = RoomListingsButtons[index];
			roomListing.SetRoomNameText(room.Name);
			roomListing.Updated = true;
		}
		
	}


	private void RemoveOldRooms()
	{
		List<RoomListing> removeRooms=new List<RoomListing>();

		foreach (RoomListing roomListing in RoomListingsButtons)
		{
			if (!roomListing.Updated)
			{
				removeRooms.Add(roomListing);
			}
			else
			{
				roomListing.Updated = false;
			}
		}

		foreach (var roomListing in removeRooms)
		{
			GameObject roomListingObj = roomListing.gameObject;
			RoomListingsButtons.Remove(roomListing);
			Destroy(roomListing);
		}
	}
}


