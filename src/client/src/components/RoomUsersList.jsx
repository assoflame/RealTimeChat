import React from "react";

const RoomUsersList = ({roomUsers}) => {
    return (
        <div>
            <div style={{fontWeight:'bold', fontSize:'29px'}}>Users:</div>
            {
                roomUsers.map((user, index) => <div key={index}>{user}</div>)
            }
        </div>
    )
}

export default RoomUsersList;