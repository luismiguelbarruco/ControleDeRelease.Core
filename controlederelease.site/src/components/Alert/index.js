import React, { useEffect } from 'react';

import './style.css';

const Alert = ({ content, isOpen, toggle }) => {
    
    useEffect(() => {
        toogleAlert(isOpen);
    }, [isOpen])

    function toogleAlert(isOpen) {
        const $modal = document.getElementsByClassName('alert-request-result')[0];
        $modal.style.display = isOpen ? 'block' : 'none';
    }

    return (
        <div className="alert-request-result">
            <div className="alert-header">
                <span className="alert-title">{content && content.message}</span>
                <button type="button" className="close-button-modal" onClick={toggle}>
                    <span>×</span>
                </button>
            </div>

            {(() => {
                if (content.data && content.data.length > 0) {
                    return (
                        <ul className="itens">
                            {content.data.map((item, index) => {
                                return <li className="item" key={index}>{item.message}</li>
                            })}
                        </ul>
                    )
                }
            })()}
        </div>
    )
}

export default Alert;