INSERT INTO SAL.CATEGORY (CATEGORY_NAME, LOG_USER_CREATE)
VALUES 
('Granos y Cereales', 'ADMIN'),
('Legumbres', 'ADMIN'),
('Lácteos', 'ADMIN'),
('Carnes y Embutidos', 'ADMIN'),
('Pescados y Mariscos', 'ADMIN'),
('Huevos y Derivados', 'ADMIN'),
('Aceites y Grasas', 'ADMIN'),
('Pan y Derivados', 'ADMIN'),
('Verduras y Hortalizas', 'ADMIN'),
('Frutas', 'ADMIN'),
('Pastas y Fideos', 'ADMIN'),
('Azúcar y Endulzantes', 'ADMIN'),
('Harinas y Panificación', 'ADMIN'),
('Condimentos y Especias', 'ADMIN'),
('Alimentos en Conserva', 'ADMIN'),
('Bebidas No Alcohólicas', 'ADMIN'),
('Limpieza General', 'ADMIN'),
('Cuidado de Ropa', 'ADMIN'),
('Higiene Personal', 'ADMIN'),
('Papel y Descartables', 'ADMIN'),
('Utensilios de Limpieza', 'ADMIN'),
('Cuidado de Mascotas', 'ADMIN'),
('Artículos de Cocina', 'ADMIN'),
('Insecticidas y Repelentes', 'ADMIN'),
('Velas y Fósforos', 'ADMIN');




INSERT INTO SAL.PRODUCT (PRODUCT_SKU, PRODUCT_NAME, CATEGORY_ID, PRODUCT_STOCK, PRODUCT_PRICE, LOG_USER_CREATE)
VALUES 
('SKU001', 'Arroz', 1, FLOOR(RAND()*(100-10+1)+10), ROUND(RAND()*(2.50-1.00)+1.00, 2), 'ADMIN'),
('SKU002', 'Quinoa', 1, FLOOR(RAND()*(100-10+1)+10), ROUND(RAND()*(4.00-2.00)+2.00, 2), 'ADMIN'),
('SKU003', 'Lentejas', 2, FLOOR(RAND()*(100-10+1)+10), ROUND(RAND()*(1.80-0.80)+0.80, 2), 'ADMIN'),
('SKU004', 'Frijoles', 2, FLOOR(RAND()*(100-10+1)+10), ROUND(RAND()*(1.50-0.70)+0.70, 2), 'ADMIN'),
('SKU005', 'Leche', 3, FLOOR(RAND()*(100-10+1)+10), ROUND(RAND()*(1.20-0.90)+0.90, 2), 'ADMIN'),
('SKU006', 'Queso', 3, FLOOR(RAND()*(100-10+1)+10), ROUND(RAND()*(3.50-2.00)+2.00, 2), 'ADMIN'),
('SKU007', 'Pollo', 4, FLOOR(RAND()*(100-10+1)+10), ROUND(RAND()*(5.00-3.00)+3.00, 2), 'ADMIN'),
('SKU008', 'Carne de Res', 4, FLOOR(RAND()*(100-10+1)+10), ROUND(RAND()*(8.00-5.00)+5.00, 2), 'ADMIN'),
('SKU009', 'Atún', 5, FLOOR(RAND()*(100-10+1)+10), ROUND(RAND()*(4.50-2.50)+2.50, 2), 'ADMIN'),
('SKU010', 'Camarones', 5, FLOOR(RAND()*(100-10+1)+10), ROUND(RAND()*(10.00-6.00)+6.00, 2), 'ADMIN'),
('SKU011', 'Huevos', 6, FLOOR(RAND()*(100-10+1)+10), ROUND(RAND()*(0.15-0.10)+0.10, 2), 'ADMIN'),
('SKU012', 'Aceite de Oliva', 7, FLOOR(RAND()*(100-10+1)+10), ROUND(RAND()*(7.00-4.00)+4.00, 2), 'ADMIN'),
('SKU013', 'Pan Integral', 8, FLOOR(RAND()*(100-10+1)+10), ROUND(RAND()*(1.50-1.00)+1.00, 2), 'ADMIN'),
('SKU014', 'Zanahorias', 9, FLOOR(RAND()*(100-10+1)+10), ROUND(RAND()*(1.00-0.50)+0.50, 2), 'ADMIN'),
('SKU015', 'Manzanas', 10, FLOOR(RAND()*(100-10+1)+10), ROUND(RAND()*(1.20-0.80)+0.80, 2), 'ADMIN'),
('SKU016', 'Fideos', 11, FLOOR(RAND()*(100-10+1)+10), ROUND(RAND()*(1.30-0.90)+0.90, 2), 'ADMIN'),
('SKU017', 'Azúcar', 12, FLOOR(RAND()*(100-10+1)+10), ROUND(RAND()*(1.00-0.70)+0.70, 2), 'ADMIN'),
('SKU018', 'Harina de Trigo', 13, FLOOR(RAND()*(100-10+1)+10), ROUND(RAND()*(1.20-0.80)+0.80, 2), 'ADMIN'),
('SKU019', 'Sal', 14, FLOOR(RAND()*(100-10+1)+10), ROUND(RAND()*(0.50-0.20)+0.20, 2), 'ADMIN'),
('SKU020', 'Maíz enlatado', 15, FLOOR(RAND()*(100-10+1)+10), ROUND(RAND()*(2.00-1.20)+1.20, 2), 'ADMIN'),
('SKU021', 'Jugo de Naranja', 16, FLOOR(RAND()*(100-10+1)+10), ROUND(RAND()*(1.50-0.90)+0.90, 2), 'ADMIN'),
('SKU022', 'Desinfectante', 17, FLOOR(RAND()*(100-10+1)+10), ROUND(RAND()*(3.00-1.50)+1.50, 2), 'ADMIN'),
('SKU023', 'Detergente para ropa', 18, FLOOR(RAND()*(100-10+1)+10), ROUND(RAND()*(2.50-1.50)+1.50, 2), 'ADMIN'),
('SKU024', 'Champú', 19, FLOOR(RAND()*(100-10+1)+10), ROUND(RAND()*(2.00-1.20)+1.20, 2), 'ADMIN'),
('SKU025', 'Papel Higiénico', 20, FLOOR(RAND()*(100-10+1)+10), ROUND(RAND()*(1.50-1.00)+1.00, 2), 'ADMIN'),
('SKU026', 'Esponja', 21, FLOOR(RAND()*(100-10+1)+10), ROUND(RAND()*(0.80-0.30)+0.30, 2), 'ADMIN'),
('SKU027', 'Alimento para perros', 22, FLOOR(RAND()*(100-10+1)+10), ROUND(RAND()*(3.50-2.00)+2.00, 2), 'ADMIN'),
('SKU028', 'Papel Aluminio', 23, FLOOR(RAND()*(100-10+1)+10), ROUND(RAND()*(1.50-1.00)+1.00, 2), 'ADMIN'),
('SKU029', 'Insecticida', 24, FLOOR(RAND()*(100-10+1)+10), ROUND(RAND()*(4.00-2.50)+2.50, 2), 'ADMIN'),
('SKU030', 'Vela', 25, FLOOR(RAND()*(100-10+1)+10), ROUND(RAND()*(1.20-0.50)+0.50, 2), 'ADMIN');


INSERT INTO SAL.STATUS_ORDER (STATUS_ORDER_NAME, STATUS_ORDER_DESC, LOG_USER_CREATE)
VALUES 
    ('Por atender', 'Estado para el momento del registro', 'ADMIN'),
    ('En proceso', 'Estado que el Encargado colocará después del registro', 'ADMIN'),
    ('Entregado', 'Estado que el Delivery colocará al momento de la entrega', 'ADMIN');

	INSERT INTO SAL.NUMBERING_TYPE (NUM_TYPE_NAME, NUM_TYPE_DESC, LOG_USER_CREATE)
VALUES 
    ('FA', 'Factura', 'ADMIN'),
    ('BO', 'Boleta', 'ADMIN');


INSERT INTO SAL.NUMBERING (NUM_TYPE_ID, NUM_SERIAL, NUM_NAME, NUM_NOW, LOG_USER_CREATE)
VALUES 
    ((SELECT NUM_TYPE_ID FROM SAL.NUMBERING_TYPE WHERE NUM_TYPE_NAME = 'FA'), 'F101', 'Factura tienda principal', 1, 'ADMIN'),
    ((SELECT NUM_TYPE_ID FROM SAL.NUMBERING_TYPE WHERE NUM_TYPE_NAME = 'FA'), 'F201', 'Factura sucursal lima', 1, 'ADMIN');

INSERT INTO SAL.NUMBERING (NUM_TYPE_ID, NUM_SERIAL, NUM_NAME, NUM_NOW, LOG_USER_CREATE)
VALUES 
    ((SELECT NUM_TYPE_ID FROM SAL.NUMBERING_TYPE WHERE NUM_TYPE_NAME = 'BO'), 'B101', 'Boleta tienda principal', 1, 'ADMIN'),
    ((SELECT NUM_TYPE_ID FROM SAL.NUMBERING_TYPE WHERE NUM_TYPE_NAME = 'BO'), 'B201', 'Boleta sucursal lima', 1, 'ADMIN');


INSERT INTO SEG.MODULE(MODULE_NAME,MODULE_ICON,MODULE_ROUTE, LOG_USER_CREATE) values
('DashBoard','dashboard','/pages/dashboard','ADMIN'),
('Usuarios','group','/pages/usuarios','ADMIN'),
('Productos','collections_bookmark','/pages/productos','ADMIN'),
('Venta','currency_exchange','/pages/venta','ADMIN'),
('Historial Ventas','edit_note','/pages/historial_venta','ADMIN'),
('Reportes','receipt','/pages/reportes','ADMIN')

INSERT INTO SEG.MODULE_ROL(MODULE_ID,ROL_ID, LOG_USER_CREATE) values
(1,1,'ADMIN'),
(2,1,'ADMIN'),
(3,1,'ADMIN'),
(4,1,'ADMIN'),
(5,1,'ADMIN'),
(6,1,'ADMIN'),
(4,2,'ADMIN'),
(5,2,'ADMIN'),
(3,3,'ADMIN'),
(4,3,'ADMIN'),
(5,3,'ADMIN'),
(6,3,'ADMIN')


