����   1 �  com/cardmng/form/MayRunAtFirst  java/lang/Object str Ljava/lang/String; table Ljava/util/Hashtable; 	Signature ;Ljava/util/Hashtable<Ljava/lang/String;Ljava/lang/Object;>; <init> ()V Code
       	    	      org/dom4j/io/SAXReader
    javax/naming/InitialContext
    	state.xml
  !   java/lang/Class " # getResourceAsStream )(Ljava/lang/String;)Ljava/io/InputStream;
  % & ' read +(Ljava/io/InputStream;)Lorg/dom4j/Document; ) + * org/dom4j/Document , - getRootElement ()Lorg/dom4j/Element;
 / 1 0 org/dom4j/DocumentException 2  printStackTrace
 4 1 5 javax/naming/NamingException 7 9 8 org/dom4j/Element : ; elements ()Ljava/util/List; = ? > java/util/List @ A iterator ()Ljava/util/Iterator; C E D java/util/Iterator F G next ()Ljava/lang/Object; I java/util/HashMap
 H  L id 7 N O P attributeValue &(Ljava/lang/String;)Ljava/lang/String; 7 R S T getText ()Ljava/lang/String;
 H V W X put 8(Ljava/lang/Object;Ljava/lang/Object;)Ljava/lang/Object; C Z [ \ hasNext ()Z ^ ` _ javax/naming/Context a b