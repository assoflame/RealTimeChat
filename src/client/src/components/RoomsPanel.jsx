import React, { useState } from "react";
import RoomsList from "./RoomsList";
import '../styles/RoomsList.css'

const RoomsPanel = ({rooms, createRoom, joinRoom}) => {
    const [roomName, setRoomName] = useState('');
    const [searchRooms, setSearchRooms] = useState(rooms);

    const searchRoom = (roomName) => {
        let relevantRooms = rooms.filter(room => room.name.startsWith(roomName));
        setSearchRooms(relevantRooms);
        console.log('search');
    }

    return (
        <div id="roomsPanel">
            <div id='roomSearch'>
                <input id='roomSearchInput' placeholder="Room name" type="text"
                    onChange={e => {setRoomName(e.target.value); searchRoom(e.target.value)}} value={roomName}></input>
                <button id="createRoomButton" onClick={e => createRoom(roomName)}>Create room</button>
            </div>
            <RoomsList rooms={searchRooms} joinRoom={joinRoom}/>
        </div>
    );
}

export default RoomsPanel;