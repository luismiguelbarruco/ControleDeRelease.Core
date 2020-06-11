import React from 'react'

const HeaderSection = (props) => {
    return (
        <header className="header-section">
            <h1>{props.title}</h1>
        </header>
    )
}

export default HeaderSection;