import React, { useEffect } from 'react';
import loaderImg from "../../assets/images/loader.gif";
import './style.css';

const PageLoader = ({ loading }) => {

    useEffect(() => {
        toogleAlert(loading);
    }, [loading]);

    function toogleAlert(loading) {
        const loadingContainer = document.getElementById('loading-container');
        loadingContainer.style.display = loading ? 'block' : 'none';
    }

    return (
        <div className="loading-container" id="loading-container">
            <div className="loader">
                <img src={loaderImg} alt="loading"/>
            </div>
        </div>
    )
}

export default PageLoader;