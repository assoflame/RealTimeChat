import React from "react";
import '../styles/RoomsList.css'

const RoomsList = ({rooms, joinRoom}) => {
    return (
        <div id="roomsList">
            {rooms.length !== 0
                ? rooms.map((room, index) => <div key={index} className="roomLink" onClick={e => joinRoom(room.name)}> {room.name} </div>)
                : <div></div>}
        </div>
    );
}

export default RoomsList;