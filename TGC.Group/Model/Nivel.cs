using System.Collections.Generic;
using System.Linq;
using TGC.Core.BoundingVolumes;
using TGC.Core.Direct3D;
using TGC.Core.Geometry;
using TGC.Core.Mathematica;
using TGC.Core.SceneLoader;
using TGC.Core.Textures;

namespace TGC.Group.Model {
    class Nivel {

        List<TgcPlane> pisosNormales;
        List<TgcPlane> pisosResbaladizos;
        List<TgcPlane> pMuerte;
        List<Caja> cajas;
        List<Plataforma> pEstaticas;
        List<PlataformaDesplazante> pDesplazan;
        List<PlataformaRotante> pRotantes;
        List<PlataformaAscensor> pAscensor;
        // Objetos decorativos
        private TgcMesh calavera;
        private TgcMesh faraon;
        private TgcMesh arbolBananas;
        private TgcMesh palmera;
        List<TgcMesh> decorativos;
        List<TgcBoundingAxisAlignBox> aabbDeDecorativos;

        public Nivel(string mediaDir) {

            // Listas de pisos
            pisosNormales = new List<TgcPlane>();
            pisosResbaladizos = new List<TgcPlane>();
            pMuerte = new List<TgcPlane>();

            // Listas de objetos del escenario
            cajas = new List<Caja>();
            pEstaticas = new List<Plataforma>();
            pDesplazan = new List<PlataformaDesplazante>();
            pRotantes = new List<PlataformaRotante>();
            pAscensor = new List<PlataformaAscensor>();
            decorativos = new List<TgcMesh>();
            aabbDeDecorativos = new List<TgcBoundingAxisAlignBox>();

            // Loader de los objetos
            var loader = new TgcSceneLoader();

            // Texturas utilizadas
            var pisoTexture = TgcTexture.createTexture(D3DDevice.Instance.Device, mediaDir + "pisoJungla.jpg");
            var hieloTexture = TgcTexture.createTexture(D3DDevice.Instance.Device, mediaDir + "hielo.jpg");
            var cajaTexture = TgcTexture.createTexture(D3DDevice.Instance.Device, mediaDir + "caja.jpg");
            var paredJunglaTexture = TgcTexture.createTexture(D3DDevice.Instance.Device, mediaDir + "paredJungla.jpg");
            var desiertoTexture = TgcTexture.createTexture(D3DDevice.Instance.Device, mediaDir + "arena.jpg");
            var piedraTexture = TgcTexture.createTexture(D3DDevice.Instance.Device, mediaDir + "piedra.png");
            var precipicioTexture = TgcTexture.createTexture(D3DDevice.Instance.Device, mediaDir + "precipicio.jpg");
            var maderaTexture = TgcTexture.createTexture(D3DDevice.Instance.Device, mediaDir + "tronco.jpg");
            var endingTexture = TgcTexture.createTexture(D3DDevice.Instance.Device, mediaDir + "damero.png");

            // Cargo objetos decorativos
            var escenaCalavera1 = loader.loadSceneFromFile(mediaDir + "\\Decorativos\\Calabera\\Calabera-TgcScene.xml");
            var escenaCalavera2 = loader.loadSceneFromFile(mediaDir + "\\Decorativos\\Calabera\\Calabera-TgcScene.xml");
            var escenaCalavera3 = loader.loadSceneFromFile(mediaDir + "\\Decorativos\\Calabera\\Calabera-TgcScene.xml");
            var escenaArbolBananas = loader.loadSceneFromFile(mediaDir + "\\Decorativos\\ArbolBananas\\ArbolBananas-TgcScene.xml");
            var escenaFaraon1 = loader.loadSceneFromFile(mediaDir + "\\Decorativos\\EstatuaFaraon\\EstatuaFaraon-TgcScene.xml");
            var escenaFaraon2 = loader.loadSceneFromFile(mediaDir + "\\Decorativos\\EstatuaFaraon\\EstatuaFaraon-TgcScene.xml");
            var escenaPalmera = loader.loadSceneFromFile(mediaDir + "\\Decorativos\\Palmera\\Palmera-TgcScene.xml");
            cargarDecorativo(calavera, escenaCalavera1, new TGCVector3(350, -180, 5200), new TGCVector3(1, 1, 1), FastMath.PI_HALF);
            cargarDecorativo(calavera, escenaCalavera2, new TGCVector3(350, -180, 5000), new TGCVector3(1, 1, 1), FastMath.PI_HALF);
            cargarDecorativo(calavera, escenaCalavera3, new TGCVector3(350, -180, 4800), new TGCVector3(1, 1, 1), FastMath.PI_HALF);
            cargarDecorativo(arbolBananas, escenaArbolBananas, new TGCVector3(390, 0, -300), new TGCVector3(1.5f, 1.5f, 1.5f), 0);
            cargarDecorativo(faraon, escenaFaraon1, new TGCVector3(-400, -180, 2700), new TGCVector3(0.3f, 0.3f, 0.3f), FastMath.PI);
            cargarDecorativo(faraon, escenaFaraon2, new TGCVector3(400, -180, 2700), new TGCVector3(0.3f, 0.3f, 0.3f), FastMath.PI);
            cargarDecorativo(palmera, escenaPalmera, new TGCVector3(650, 0, 1700), new TGCVector3(0.7f, 0.7f, 0.7f), 0);

            // Piso de la jungla
            var piso = new TgcPlane(new TGCVector3(-500, 0, -500), new TGCVector3(2500, 0, 2500), TgcPlane.Orientations.XZplane, pisoTexture);
            pisosNormales.Add(piso); 

            // Pisos del desierto
            piso = new TgcPlane(new TGCVector3(-500, -180, 2600), new TGCVector3(1000, 0, 700), TgcPlane.Orientations.XZplane, desiertoTexture);
            pisosNormales.Add(piso); 
            piso = new TgcPlane(new TGCVector3(-500, -180, 3300), new TGCVector3(350, 0, 800), TgcPlane.Orientations.XZplane, desiertoTexture);
            pisosNormales.Add(piso);
            piso = new TgcPlane(new TGCVector3(350, -180, 3300), new TGCVector3(150, 0, 2800), TgcPlane.Orientations.XZplane, desiertoTexture);
            pisosNormales.Add(piso);
            piso = new TgcPlane(new TGCVector3(-150, -180, 3500), new TGCVector3(500, 0, 2600), TgcPlane.Orientations.XZplane, desiertoTexture);
            pisosNormales.Add(piso);
            piso = new TgcPlane(new TGCVector3(-500, -180, 4100), new TGCVector3(100, 0, 2000), TgcPlane.Orientations.XZplane, desiertoTexture);
            pisosNormales.Add(piso);
            piso = new TgcPlane(new TGCVector3(-400, -180, 4900), new TGCVector3(250, 0, 1200), TgcPlane.Orientations.XZplane, desiertoTexture);
            pisosNormales.Add(piso);

            // Piso de hielo
            piso = new TgcPlane(new TGCVector3(-500, 0, -3000), new TGCVector3(2500, 0, 2500), TgcPlane.Orientations.XZplane, hieloTexture);
            pisosResbaladizos.Add(piso);

            // Paredes de la jungla
            pEstaticas.Add(new Plataforma(new TGCVector3(-500, 150, 600), new TGCVector3(100, 300, 2800), paredJunglaTexture)); //laterales jungla derecha
            pEstaticas.Add(new Plataforma(new TGCVector3(500, 150, 600), new TGCVector3(100, 300, 2800), paredJunglaTexture));
            pEstaticas.Add(new Plataforma(new TGCVector3(1975, 150, 0), new TGCVector3(50, 300, 1600), paredJunglaTexture)); // borde izquierdo jungla derecha
            pEstaticas.Add(new Plataforma(new TGCVector3(1250, 30, 1990), new TGCVector3(1500, 60, 20), paredJunglaTexture)); // fondo jungla izquierda

            // Paredes del desierto; el desierto está a un nivel inferior que la jungla y los glaciares
            pEstaticas.Add(new Plataforma(new TGCVector3(500, -150, 4350), new TGCVector3(100, 60, 3500), desiertoTexture));
            pEstaticas.Add(new Plataforma(new TGCVector3(-500, -150, 4350), new TGCVector3(100, 60, 3500), desiertoTexture));
            pEstaticas.Add(new Plataforma(new TGCVector3(0, -165, 6090), new TGCVector3(900, 70, 20), desiertoTexture));

            // Pecipicios del desierto
            piso = new TgcPlane(new TGCVector3(-150, -500, 3300), new TGCVector3(500, 0, 200), TgcPlane.Orientations.XZplane, precipicioTexture);
            pMuerte.Add(piso); // precipicio ancho
            piso = new TgcPlane(new TGCVector3(-400, -500, 4100), new TGCVector3(250, 0, 800), TgcPlane.Orientations.XZplane, precipicioTexture);
            pMuerte.Add(piso); // precipicio largo
            pEstaticas.Add(new Plataforma(new TGCVector3(100, -340, 3300), new TGCVector3(500, 320, 2), precipicioTexture)); // paredes precipicio ancho
            pEstaticas.Add(new Plataforma(new TGCVector3(350, -340, 3400), new TGCVector3(2, 320, 200), precipicioTexture));
            pEstaticas.Add(new Plataforma(new TGCVector3(-150, -340, 3400), new TGCVector3(2, 320, 200), precipicioTexture));
            pEstaticas.Add(new Plataforma(new TGCVector3(100, -340, 3500), new TGCVector3(500, 320, 2), precipicioTexture));
            pEstaticas.Add(new Plataforma(new TGCVector3(-275, -340, 4100), new TGCVector3(250, 320, 2), precipicioTexture)); // paredes precipicio largo
            pEstaticas.Add(new Plataforma(new TGCVector3(-150, -340, 4500), new TGCVector3(2, 320, 800), precipicioTexture));
            pEstaticas.Add(new Plataforma(new TGCVector3(-400, -340, 4500), new TGCVector3(2, 320, 800), precipicioTexture));
            pEstaticas.Add(new Plataforma(new TGCVector3(-275, -340, 4900), new TGCVector3(250, 320, 2), precipicioTexture));

            // Escalinatas de piedra, separan jungla de desierto
            var tamanioEscalinata = new TGCVector3(900, 60, 200);
            pEstaticas.Add(new Plataforma(new TGCVector3(0, -150, 2500), tamanioEscalinata, piedraTexture));  // escalinata inferior
            pEstaticas.Add(new Plataforma(new TGCVector3(0, -90, 2300), tamanioEscalinata, piedraTexture));   // escalinata del medio
            pEstaticas.Add(new Plataforma(new TGCVector3(0, -30, 2100), tamanioEscalinata, piedraTexture));   // escalinata superior
            pEstaticas.Add(new Plataforma(new TGCVector3(500, -140, 2500), new TGCVector3(100, 80, 200), piedraTexture));  // contornos inferior
            pEstaticas.Add(new Plataforma(new TGCVector3(-500, -140, 2500), new TGCVector3(100, 80, 200), piedraTexture));
            pEstaticas.Add(new Plataforma(new TGCVector3(500, -115, 2300), new TGCVector3(100, 130, 200), piedraTexture));  // contornos del medio
            pEstaticas.Add(new Plataforma(new TGCVector3(-500, -115, 2300), new TGCVector3(100, 130, 200), piedraTexture));
            pEstaticas.Add(new Plataforma(new TGCVector3(500, -80, 2100), new TGCVector3(100, 200, 200), piedraTexture));  // contornos superior
            pEstaticas.Add(new Plataforma(new TGCVector3(-500, -80, 2100), new TGCVector3(100, 200, 200), piedraTexture));

            // Paredes de los glaciares
            pEstaticas.Add(new Plataforma(new TGCVector3(-500, 200, -1900), new TGCVector3(100, 400, 2200), hieloTexture)); // derecha
            pEstaticas.Add(new Plataforma(new TGCVector3(750, 200, -2510), new TGCVector3(2500, 400, 20), hieloTexture));  // fondo
            pEstaticas.Add(new Plataforma(new TGCVector3(1975, 200, -1900), new TGCVector3(50, 400, 2200), hieloTexture)); // izquierda

            // Precipicio del tronco
            piso = new TgcPlane(new TGCVector3(2000, -500, 800), new TGCVector3(1000, 0, 1200), TgcPlane.Orientations.XZplane, precipicioTexture);
            pMuerte.Add(piso); 
            pEstaticas.Add(new Plataforma(new TGCVector3(2500, -250, 800), new TGCVector3(1000, 500, 2), precipicioTexture)); // fondo
            pEstaticas.Add(new Plataforma(new TGCVector3(2000, -250, 1400), new TGCVector3(2, 500, 1200), precipicioTexture)); // derecha
            pEstaticas.Add(new Plataforma(new TGCVector3(3000, -250, 1400), new TGCVector3(2, 500, 1200), precipicioTexture)); // izquierda
            pEstaticas.Add(new Plataforma(new TGCVector3(2500, -250, 2000), new TGCVector3(1000, 500, 2), precipicioTexture)); // frontal

            // Sector post-precipicio del tronco
            piso = new TgcPlane(new TGCVector3(3000, 0, 800), new TGCVector3(1200, 0, 1200), TgcPlane.Orientations.XZplane, endingTexture);
            pisosNormales.Add(piso);
            pEstaticas.Add(new Plataforma(new TGCVector3(4190, 20, 1400), new TGCVector3(20, 40, 1200), paredJunglaTexture));
            pEstaticas.Add(new Plataforma(new TGCVector3(3600, 20, 1990), new TGCVector3(1200, 40, 20), paredJunglaTexture));
            pEstaticas.Add(new Plataforma(new TGCVector3(3600, 20, 790), new TGCVector3(1200, 40, 20), paredJunglaTexture));

            // Cajas movibles del escenario
            cajas.Add(new Caja(mediaDir, new TGCVector3(-250, 40, -1000))); 
            cajas.Add(new Caja(mediaDir, new TGCVector3(250, 40, 250)));   
            cajas.Add(new Caja(mediaDir, new TGCVector3(1250, 40, 1200)));
            cajas.Add(new Caja(mediaDir, new TGCVector3(700, 40, 0)));

            // Plataforma desplazante en XZ y tronco desplazante en X
            pDesplazan.Add(new PlataformaDesplazante(new TGCVector3(0, -50, 5000), new TGCVector3(200, 50, 200), cajaTexture, new TGCVector3(-200, -50, 5000), new TGCVector3(0.2f, 0, 0)));
            pDesplazan.Add(new PlataformaDesplazante(new TGCVector3(2075, -60, 1400), new TGCVector3(150, 50, 80), maderaTexture, new TGCVector3(2925, -60, 1400), new TGCVector3(0.2f, 0, 0)));

            // Plataforma rotante
            pRotantes.Add(new PlataformaRotante(new TGCVector3(0, 70, 300), new TGCVector3(100, 50, 100), cajaTexture, FastMath.PI * 200));
            
            // Plataforma ascensor en Y
            pAscensor.Add(new PlataformaAscensor(new TGCVector3(0, -140, 2800), new TGCVector3(200, 50, 200), cajaTexture, 200, 0.1f));
        }

        public void update(float deltaTime) {

            foreach (var p in pDesplazan) {
                p.update(deltaTime);
            }

            foreach (var p in pRotantes) {
                p.update(deltaTime);
            }

            foreach (var p in pAscensor) {
                p.update(deltaTime);
            }

        }

        public void render() {

            foreach (var piso in pisosNormales) {
                piso.Render();
            }

            foreach (var hielo in pisosResbaladizos) {
                hielo.Render();
            }

            foreach (var deathplane in pMuerte)
            {
                deathplane.Render();
            }

            foreach (var caja in cajas) {
                caja.render();
            }

            foreach (var p in pEstaticas) {
                p.render();
            }

            foreach (var p in pDesplazan) {
                p.render();
            }

            foreach (var p in pRotantes) {
                p.render();
            }

            foreach (var p in pAscensor) {
                p.render();
            }

            foreach (var decorativo in decorativos) {
                decorativo.Render();
            }

        }

        public void dispose() {

            foreach (var piso in pisosNormales) {
                piso.Dispose();
            }

            foreach (var hielo in pisosResbaladizos) {
                hielo.Dispose();
            }

            foreach (var deathplane in pMuerte)
            {
                deathplane.Dispose();
            }

            foreach (var caja in cajas) {
                caja.dispose();
            }

            foreach (var p in pEstaticas) {
                p.dispose();
            }

            foreach (var p in pDesplazan) {
                p.dispose();
            }

            foreach (var p in pRotantes) {
                p.dispose();
            }

            foreach (var p in pAscensor) {
                p.dispose();
            }

            foreach (var decorativo in decorativos) {
                decorativo.Dispose();
            }

        }

        public void cargarDecorativo (TgcMesh unDecorativo, TgcScene unaEscena, TGCVector3 posicion, TGCVector3 escala, float rotacion)
        {
            unDecorativo = unaEscena.Meshes[0];
            unDecorativo.Position = posicion;
            unDecorativo.Scale = escala;
            unDecorativo.RotateY(rotacion);
            decorativos.Add(unDecorativo);
            aabbDeDecorativos.Add(unDecorativo.BoundingBox);
        }

        public List<TgcBoundingAxisAlignBox> getBoundingBoxes() {
            var list = new List<TgcBoundingAxisAlignBox>();
            list.AddRange(getPisos().ToArray());
            list.AddRange(cajas.Select(caja => caja.getSuperior()).ToArray());
            list.AddRange(cajas.Select(caja => caja.getCuerpo()).ToArray());
            list.AddRange(pEstaticas.Select(plataforma => plataforma.getAABB()).ToArray());
            list.AddRange(pDesplazan.Select(desplazante => desplazante.getAABB()).ToArray());
            list.AddRange(pRotantes.Select(rotante => rotante.getAABB()).ToArray());
            list.AddRange(pAscensor.Select(ascensor => ascensor.getAABB()).ToArray());
            list.AddRange(aabbDeDecorativos);
            return list;
        }

        public List<TgcBoundingAxisAlignBox> getPisos() {
            var list = new List<TgcBoundingAxisAlignBox>();
            list.AddRange(pisosNormales.Select(piso => piso.BoundingBox).ToArray());
            list.AddRange(pisosResbaladizos.Select(piso => piso.BoundingBox).ToArray());
            list.AddRange(pEstaticas.Select(caja => caja.getAABB()).ToArray());
            list.AddRange(pDesplazan.Select(caja => caja.getAABB()).ToArray());
            list.AddRange(pRotantes.Select(caja => caja.getAABB()).ToArray());
            list.AddRange(pAscensor.Select(caja => caja.getAABB()).ToArray());

            return list;
        }

        public List<Caja> getCajas() {
            return cajas;
        }

        public bool esPisoResbaladizo(TgcBoundingAxisAlignBox piso) {
            return pisosResbaladizos.Select(p => p.BoundingBox).Contains(piso);
        }

        public bool esPisoDesplazante(TgcBoundingAxisAlignBox piso) {
            return pDesplazan.Select(p => p.getAABB()).Contains(piso);
        }

        public bool esPisoRotante(TgcBoundingAxisAlignBox piso) {
            return pRotantes.Select(p => p.getAABB()).Contains(piso);
        }

        public bool esPisoAscensor(TgcBoundingAxisAlignBox piso) {
            return pAscensor.Select(p => p.getAABB()).Contains(piso);
        }

        public PlataformaDesplazante getPlataformaDesplazante(TgcBoundingAxisAlignBox piso) {
            return pDesplazan.Find(p => p.getAABB() == piso);
        }

        public PlataformaRotante getPlataformaRotante(TgcBoundingAxisAlignBox piso) {
            return pRotantes.Find(p => p.getAABB() == piso);
        }

        public PlataformaAscensor getPlataformaAscensor(TgcBoundingAxisAlignBox piso) {
            return pAscensor.Find(p => p.getAABB() == piso);
        }

        public List<TgcBoundingAxisAlignBox> getDeathPlanes() {
            return pMuerte.Select(p => p.BoundingBox).ToList();
        }
    }
}
