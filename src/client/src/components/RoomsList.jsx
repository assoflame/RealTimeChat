import React, { useState } from "react";

const RoomsList = ({rooms, createRoom, joinRoom, searchRoom}) => {
    const [roomName, setRoomName] = useState('');

    return (
        <div id="roomsPanel">
            <div id='roomSearch'>
                <input id='roomSearchInput' placeholder="Room name" type="text"
                    onChange={e => {setRoomName(e.target.value); searchRoom(roomName)}} value={roomName}></input>
                <button id="createRoomButton" onClick={e => createRoom(roomName)}>Create room</button>
            </div>
            <div id="roomsList">
                {rooms.length !== 0
                    ? rooms.map((room, index) => <div className="roomLink" key={index} onClick={e => joinRoom(room.name)}> {room.name} </div>)
                    : <div></div>}
            </div>
        </div>
    );
}

export default RoomsList;