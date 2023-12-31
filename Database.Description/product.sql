PGDMP                  
    {            WebApi    16.0    16.0     �           0    0    ENCODING    ENCODING        SET client_encoding = 'UTF8';
                      false            �           0    0 
   STDSTRINGS 
   STDSTRINGS     (   SET standard_conforming_strings = 'on';
                      false            �           0    0 
   SEARCHPATH 
   SEARCHPATH     8   SELECT pg_catalog.set_config('search_path', '', false);
                      false            �           1262    16398    WebApi    DATABASE     �   CREATE DATABASE "WebApi" WITH TEMPLATE = template0 ENCODING = 'UTF8' LOCALE_PROVIDER = libc LOCALE = 'English_United States.1252';
    DROP DATABASE "WebApi";
                postgres    false            �            1259    16400    product    TABLE     �  CREATE TABLE public.product (
    id integer NOT NULL,
    code character varying(9) NOT NULL,
    name character varying(90) NOT NULL,
    category character varying(28) NOT NULL,
    brand character varying(28),
    type character varying(21),
    description character varying(180),
    created_at timestamp with time zone DEFAULT CURRENT_TIMESTAMP,
    updated_at timestamp with time zone DEFAULT CURRENT_TIMESTAMP
);
    DROP TABLE public.product;
       public         heap    postgres    false            �            1259    16399    product_id_seq    SEQUENCE     �   CREATE SEQUENCE public.product_id_seq
    AS integer
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1;
 %   DROP SEQUENCE public.product_id_seq;
       public          postgres    false    216            �           0    0    product_id_seq    SEQUENCE OWNED BY     A   ALTER SEQUENCE public.product_id_seq OWNED BY public.product.id;
          public          postgres    false    215            #           2604    16403 
   product id    DEFAULT     h   ALTER TABLE ONLY public.product ALTER COLUMN id SET DEFAULT nextval('public.product_id_seq'::regclass);
 9   ALTER TABLE public.product ALTER COLUMN id DROP DEFAULT;
       public          postgres    false    216    215    216            �          0    16400    product 
   TABLE DATA           m   COPY public.product (id, code, name, category, brand, type, description, created_at, updated_at) FROM stdin;
    public          postgres    false    216   �       �           0    0    product_id_seq    SEQUENCE SET     =   SELECT pg_catalog.setval('public.product_id_seq', 22, true);
          public          postgres    false    215            '           2606    16407    product product_pkey 
   CONSTRAINT     R   ALTER TABLE ONLY public.product
    ADD CONSTRAINT product_pkey PRIMARY KEY (id);
 >   ALTER TABLE ONLY public.product DROP CONSTRAINT product_pkey;
       public            postgres    false    216            �   z  x��WMo�8=+�b���)���d��f;��M������msM�.)5q�>�#�:E��@��S4�͛y3C1o�v�+��SR��L����jEq�&�a���w�Ѓ,�4яޅ�
�N�
k���zC��T[�b5��^O+]�6uU%�XpCJ�%骴2���"�pF��Ή[+g��ޞ��Q�Ҩ2K��sY�q-s��%���"��at��Y�"��Z;��0�ō�I����(t~��M����K�W4� }(�~(���R���U�Y�)]΅��Lȹ�ч� n(�l���y7������K*Y�4ՆxV��D�Υʌ(��DA�}�\�����Kjv�v:��D8JO��S+u������Y�
�/�$b��p�=�0xC=���5x6�ݾc�>��Bا�c�s���R�n�Pz
�$z*-���Ȣ%�+n�w!�擧]�����W�p�ݚ���.�O'�3��R[Y:�kBC���}@� %�L�!v0 �r	�܅A���fR�MZt�
��:� YD���? z(����+Y�sQPg��a�n���GD����oܩ<�������׎ٶ��{�)�Ȗ:0�B!�1B�D��0ԝ��@�B�`xV�G�P���:�F�9����P�rT�!GF���גr��g�Z+���my�9
Aؿ��ۚ��jG��֩�4z
T�7�_�� 5�x%�[>�M��!�{Z/$�B
D�jqm�%Ov}��J����mvڟQ��������F�8|x#�&�.�Z�S�ǥ4�W�^���p6Q���T%OK�,�i��{m�\�� ��\��u��w%Q���5%Y�.O���R�_�[��m+�&o�{�t.7��\�P\�A�[rt2��8�&HT�d����2G�����o؞�s���R���}�I��`�_V6"�F �L�s%U����O'p�uJ�t)]o��[�Ӎ.��TU�p5�q^��֧k, v	�m�\�����:���^v��^o��k�.�u�>HZ_�o�s��4�E�֛��z*�@���0�a`�]�s=�SL��[������v��b��}��A6����s�G;��D�I�`�{Y�^�l�Z�3��@�(��8�Y��=�� �}�q� ������`���u`;>�{�'?�t�m��I�l�4ԙ@�>��K1\�E�����y�K��%s��v��]:���b+b~3������Oޝ��]����<r:���Y���o3ӵ�0��fF�Z��~=f���o擴5���L��B���j�j~����;fn�M�nm�+�a��͌Y;l��Z�Z쥙q�f�ߊ��?��'�����Y\#���GQ�����T�:�z;�۵��[ϵ�E���=��	��y���o���(b���hd�;*
�������I�     