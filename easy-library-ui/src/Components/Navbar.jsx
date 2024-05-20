import React from 'react'
import { Link } from 'react-router-dom'

const Navbar = () => {
    return (
        <nav className="navbar navbar-expand-lg bg-body-tertiary n-secondary">
            <div className="container-fluid">
                <svg xmlns="http://www.w3.org/2000/svg" height="50" viewBox="0 -960 960 960" width="50" fill="#E2D4BA">
                    <path
                        d="M480-160q-48-38-104-59t-116-21q-42 0-82.5 11T100-198q-21 11-40.5-1T40-234v-482q0-11 5.5-21T62-752q46-24 96-36t102-12q58 0 113.5 15T480-740v484q51-32 107-48t113-16q36 0 70.5 6t69.5 18v-480q15 5 29.5 10.5T898-752q11 5 16.5 15t5.5 21v482q0 23-19.5 35t-40.5 1q-37-20-77.5-31T700-240q-60 0-116 21t-104 59Zm80-200v-380l200-200v400L560-360Zm-160 65v-396q-33-14-68.5-21.5T260-720q-37 0-72 7t-68 21v397q35-13 69.5-19t70.5-6q36 0 70.5 6t69.5 19Zm0 0v-396 396Z" />
                </svg>
                <a className="navbar-brand ms-2 me-3 fs-2" href="/">EasyLibrary</a>
                <button className="navbar-toggler" type="button" data-bs-toggle="collapse"
                    data-bs-target="#navbarSupportedContent" aria-controls="navbarSupportedContent" aria-expanded="false"
                    aria-label="Toggle navigation">
                    <span className="navbar-toggler-icon"></span>
                </button>
                <div className="collapse navbar-collapse" id="navbarSupportedContent">
                    <ul className="navbar-nav me-auto mb-2 mb-lg-0 fs-5">
                        <li className="nav-item">
                            <Link className="nav-link" aria-current="page" to="/">Главная</Link>
                        </li>
                        <li className="nav-item">
                            <Link className="nav-link" to="/about">О библиотеке</Link>
                        </li>
                        <li className="nav-item">
                            <Link className="nav-link" to="/contact">Напишите нам!</Link>
                        </li>
                    </ul>
                    <div className="d-flex flex-column align-items-center me-2">
                        <svg xmlns="http://www.w3.org/2000/svg" height="32" viewBox="0 -960 960 960" width="32"
                            fill="#E2D4BA">
                            <path
                                d="M240.924-268.307q51-37.846 111.115-59.769Q412.154-349.999 480-349.999t127.961 21.923q60.115 21.923 111.115 59.769 37.308-41 59.116-94.923Q800-417.154 800-480q0-133-93.5-226.5T480-800q-133 0-226.5 93.5T160-480q0 62.846 21.808 116.77 21.808 53.923 59.116 94.923Zm239.088-181.694q-54.781 0-92.396-37.603-37.615-37.604-37.615-92.384 0-54.781 37.603-92.396 37.604-37.615 92.384-37.615 54.781 0 92.396 37.603 37.615 37.604 37.615 92.384 0 54.781-37.603 92.396-37.604 37.615-92.384 37.615Zm-.012 350q-79.154 0-148.499-29.77-69.346-29.769-120.654-81.076-51.307-51.308-81.076-120.654-29.77-69.345-29.77-148.499t29.77-148.499q29.769-69.346 81.076-120.654 51.308-51.307 120.654-81.076 69.345-29.77 148.499-29.77t148.499 29.77q69.346 29.769 120.654 81.076 51.307 51.308 81.076 120.654 29.77 69.345 29.77 148.499t-29.77 148.499q-29.769 69.346-81.076 120.654-51.308 51.307-120.654 81.076-69.345 29.77-148.499 29.77ZM480-160q54.154 0 104.423-17.423 50.27-17.423 89.27-48.731-39-30.154-88.116-47Q536.462-290.001 480-290.001q-56.462 0-105.77 16.654-49.308 16.654-87.923 47.193 39 31.308 89.27 48.731Q425.846-160 480-160Zm0-349.999q29.846 0 49.924-20.077 20.077-20.078 20.077-49.924t-20.077-49.924Q509.846-650.001 480-650.001t-49.924 20.077Q409.999-609.846 409.999-580t20.077 49.924q20.078 20.077 49.924 20.077ZM480-580Zm0 355Z" />
                        </svg>
                        <Link className="nav-link p-0 fs-5" to="login">Войти</Link>
                    </div>
                </div>
            </div>
        </nav>
    );
}

export default Navbar;