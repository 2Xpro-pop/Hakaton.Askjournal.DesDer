class Navmenu extends React.Component {
    constructor(props) {
        super(props);
    }

    render() {

        return (
            <div className="post-block">
                <div className="post-block-content" >
                    <nav>
                        <div className="w-100 rounded border border-2" id="bottomNavbar">
                            <ul
                                className="navbar-nav d-flex justify-content-around flex-row w-100 py-3 tlp-bootombar"
                                style={{ fontSize: "12px", textAlign: "center" }}
                            >
                                <li className="nav-item" style={{ width: "min-content" }}>
                                    <a
                                        className="nav-link d-flex flex-column align-items-center justify-content-center"
                                        style={{ width: "min-content" }}
                                        aria-current="page"
                                        href="dobot"
                                    >
                                        <img
                                            src="/imgs/kirka.png"
                                            alt="kirka"
                                            className="img-fluid"
                                            style={{ width: "24px", height: "25px" }}
                                        />
                                        <p className="mb-0">Добывающая отрасль</p >
                                    </a>
                                </li>

                                <li className="nav-item" style={{ width: "min-content" }}>
                                    <a
                                        className="nav-link d-flex flex-column align-items-center justify-content-center"
                                        aria-current="page"
                                        href="post"
                                    >
                                        <img
                                            src="/imgs/post.png"
                                            alt="post"
                                            className="img-fluid"
                                            style={{ width: "24px", height: "25px" }}
                                        />
                                        <p className="mb-0">Посты</p>
                                    </a>
                                </li>

                                <li className="nav-item" style={{ width: "min-content" }}>
                                    <a
                                        className="nav-link d-flex flex-column align-items-center justify-content-center"
                                        aria-current="page"
                                        href="vlast"
                                    >
                                        <img
                                            src="/imgs/vlast.png"
                                            alt="vlast"
                                            className="img-fluid"
                                            style={{ width: "24px", height: "25px" }}
                                        />
                                        <p className="mb-0">Власть</p>
                                    </a>
                                </li>

                                <li
                                    className="nav-item d-flex align-items-center"
                                    style={{ width: "min-content" }}
                                >
                                    <a
                                        className="nav-link d-flex flex-column align-items-center justify-content-center"
                                        aria-current="page"
                                        href="instruction"
                                    >
                                        <img
                                            src="/imgs/pravovaya.png"
                                            alt="pravovaya"
                                            className="img-fluid"
                                            style={{ width: "24px", height: "25px" }}
                                        />
                                        <p className="mb-0">Правовая регуляция</p>
                                    </a>
                                </li>

                                <li
                                    className="nav-item d-flex align-items-center"
                                    style={{ width: "min-content" }}
                                >
                                    <a
                                        className="nav-link d-flex flex-column align-items-center justify-content-center"
                                        aria-current="page"
                                        href="prava"
                                    >
                                        <img
                                            src="/imgs/instructions.png"
                                            alt="instructions"
                                            className="img-fluid"
                                            style={{ width: "24px", height: "25px" }}
                                        />
                                        <p className="mb-0">Инструкции</p>
                                    </a>
                                </li>
                            </ul>
                        </div>
                    </nav>
                </div>
            </div>
        );
    }
}