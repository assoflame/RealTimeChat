import React, { useState } from "react";

const RoomsList = ({rooms, createRoom, joinRoom}) => {
    const [roomName, setRoomName] = useState('');

    return (
        <div>
            <div>
                <input placeholder="room name" type="text" onChange={e => setRoomName(e.target.value)} value={roomName}></input>
                <button onClick={e => createRoom(roomName)}>Create room</button>
            </div>
            <div>
                {rooms.length !== 0
                    ? rooms.map((room, index) => <div key={index} onClick={e => joinRoom(room.name)}> {room.name} </div>)
                    : <div></div>}
            </div>
        </div>
    );
}

export default RoomsList;