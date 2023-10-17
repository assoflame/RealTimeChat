import React from "react";

import '../styles/RoomUsersList.css'

const RoomUsersList = ({roomUsers, adminRights, blockUser}) => {
    return (
        <div id="roomUsers">
            <div id="roomUsersTitle">Room users</div>
            {roomUsers.filter((username) => username !== localStorage.getItem('nickname'))
                .map((user, index) => {
                    return  <div key={index} className="roomUser">
                                <div className="roomUserName">{user}</div>
                                {adminRights && <button onClick={e => blockUser(user)} className="blockUserButton">Block</button>}
                            </div>;
                            })
            }
        </div>
    )
}

export default RoomUsersList;